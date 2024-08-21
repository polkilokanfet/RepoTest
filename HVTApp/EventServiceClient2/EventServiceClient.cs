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

        public event Action StartEvent;

        public async Task Start()
        {
            if (await Ping() == false)
            {
                await Stop();

                StartEvent?.Invoke();

                //инициализация клиента сервиса
                EventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);

                try
                {
                    //подсоединяемся к сервису
                    await EventServiceHost.ConnectAsync(AppSessionId, _userId, _userRole);
                }
                catch
                {
                    // ignored
                }
            }

            await Task.Delay(new TimeSpan(0, 0, 0, 600));
            await Start();
        }

        public async Task Stop()
        {
            if (HostIsEnabled)
            {
                try
                {
                    await EventServiceHost.DisconnectAsync(AppSessionId);
                }
                catch
                {
                    // ignored
                }
            }

            //сносим хост
            EventServiceHost?.Abort();
            EventServiceHost = null;
        }


        private async Task<bool> Ping()
        {
            if (HostIsEnabled)
            {
                try
                {
                    if (await EventServiceHost.HostIsAliveAsync())
                    {
                        return true;
                    }
                }
                catch
                {
                    // ignored
                }
            }

            return false;
        }

        public bool UserConnected(Guid userId)
        {
            //if (HostIsEnabled)
            //{
            //    try
            //    {
            //        return EventServiceHost.UserIsConnected(userId);
            //    }
            //    catch (CommunicationObjectFaultedException)
            //    {
            //        this.StopWaitStart();
            //        return false;
            //    }
            //}

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
                    await Stop();
                }
            }

            return false;
        }
    }
}