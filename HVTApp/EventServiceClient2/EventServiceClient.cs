using System;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;

namespace EventServiceClient2
{
    public partial class EventServiceClient : IEventServiceClient, ISendNotificationThroughApp, EventServiceClient2.ServiceReference1.IEventServiceCallback
    {
        /// <summary>
        /// Идентификатор текущего приложения
        /// </summary>
        private Guid AppSessionId { get; } = Guid.NewGuid();

        private readonly IUnityContainer _container;
        private readonly INotificationFromDataBaseService _notificationFromDataBaseService;
        private readonly Guid _userId = GlobalAppProperties.User.Id;
        private readonly Role _userRole = GlobalAppProperties.User.RoleCurrent;

        private readonly EndpointAddress _endpointAddress = new EndpointAddress(EventServiceAddresses.TcpBaseAddress);
        private readonly NetTcpBinding _netTcpBinding = new NetTcpBinding(SecurityMode.None, true);

        /// <summary>
        /// Хост сервиса подключен
        /// </summary>
        private bool HostIsEnabled =>
            EventServiceHost != null &&
            EventServiceHost.State != CommunicationState.Faulted &&
            EventServiceHost.State != CommunicationState.Closed;

        private ServiceReference1.EventServiceClient EventServiceHost { get; set; }

        public EventServiceClient(IUnityContainer container)
        {
            _container = container;
            _notificationFromDataBaseService = container.Resolve<INotificationFromDataBaseService>();

            //увеличиваем таймаут бездействия
            //SendTimeout = new TimeSpan(7, 0, 0, 0),
            _netTcpBinding.ReceiveTimeout = new TimeSpan(7, 0, 0, 0);
            //OpenTimeout = new TimeSpan(7, 0, 0, 0),
            //CloseTimeout = new TimeSpan(7, 0, 0, 0)
        }

        public event Action StartActionInProgressEvent;

        public async Task<bool> Start()
        {
            this.StartActionInProgressEvent?.Invoke();

            var result = await this.ConnectAsync();
            if (result == false)
                this.StopWaitRestart(); //очистить следы от предыдущего подключения, подождать и рестартануть

            return result;
        }

        private async Task<bool> ConnectAsync()
        {
            //не нужно реконектится к рабочему сервису
            if (HostIsEnabled && await EventServiceHost.HostIsAliveAsync())
            {
                PingHost();
                return true;
            }

            //инициализация клиента сервиса
            EventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);

            var result = false;
            try
            {
                //подсоединяемся к сервису
                result = await EventServiceHost.ConnectAsync(AppSessionId, _userId, _userRole);
            }
            catch
            {
                // ignored
            }

            if (result == true)
                PingHost(); //циклический пинг хоста

            return result;
        }

        public async Task Stop()
        {
            try
            {
                if (HostIsEnabled)
                {
                    await EventServiceHost.DisconnectAsync(AppSessionId);
                }
            }
            catch (Exception e)
            {
                _container.Resolve<IHvtAppLogger>().LogError(e.GetType().Name, e);
            }

            //сносим хост
            EventServiceHost?.Abort();
            EventServiceHost = null;
        }

        /// <summary>
        /// Очистить старое, подождать, рестартовать
        /// </summary>
        private async void StopWaitRestart()
        {
            await Stop();
            //Thread.Sleep(new TimeSpan(0, 0, 0, 5));
            await this.Start();
        }

        #region Ping

        /// <summary>
        /// Пинг хоста (циклический при удаче)
        /// </summary>
        private void PingHost()
        {
            return;
            //new Action(() =>
            //{
            //    if (HostIsEnabled)
            //    {
            //        try
            //        {
            //            if (EventServiceHost.HostIsAlive())
            //            {
            //                this.PingHost();
            //                return;
            //            }
            //        }
            //        catch
            //        {
            //            // ignored
            //        }
            //    }

            //    this.StopWaitRestart();
            //}).SleepThenExecuteInAnotherThread(60);
        }

        #endregion

        public bool UserConnected(Guid userId)
        {
            if (HostIsEnabled)
            {
                try
                {
                    return EventServiceHost.UserIsConnected(userId);
                }
                catch (CommunicationObjectFaultedException)
                {
                    this.StopWaitRestart();
                    return false;
                }
            }

            return false;
        }

        public async Task<bool> SendNotificationAsync(NotificationUnit unit)
        {
            if (this.HostIsEnabled)
            {
                try
                {
                    var senderId = unit.SenderUser?.Id ?? unit.SenderUserId;
                    var recipientId = unit.RecipientUser?.Id ?? unit.RecipientUserId;
                    return await EventServiceHost.SendNotificationToServiceAsync(
                        this.AppSessionId,
                        senderId,
                        recipientId,
                        unit.RecipientRole,
                        unit.TargetEntityId,
                        unit.ActionType);
                }
                catch
                {
                    StopWaitRestart();
                }
            }

            return false;
        }
    }
}