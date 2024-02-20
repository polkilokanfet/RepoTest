using System;
using System.ServiceModel;
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
        private readonly IUnityContainer _container;
        private readonly INotificationGeneratorService _notificationGeneratorService;
        private readonly IPopupNotificationsService _popupNotificationsService;
        private readonly Guid _userId = GlobalAppProperties.User.Id;
        private readonly Role _userRole = GlobalAppProperties.User.RoleCurrent;

        private readonly EndpointAddress _endpointAddress = new EndpointAddress(EventServiceAddresses.TcpBaseAddress);
        private readonly NetTcpBinding _netTcpBinding = new NetTcpBinding(SecurityMode.None, true);

        /// <summary>
        /// Хост сервиса подключен
        /// </summary>
        private bool HostIsEnabled => EventServiceHost != null && 
                                      EventServiceHost.State != CommunicationState.Faulted && 
                                      EventServiceHost.State != CommunicationState.Closed;

        private Guid _appSessionId;

        private ServiceReference1.EventServiceClient EventServiceHost { get; set; }

        public EventServiceClient(IUnityContainer container)
        {
            _container = container;
            _notificationGeneratorService = container.Resolve<INotificationGeneratorService>();
            _popupNotificationsService = container.Resolve<IPopupNotificationsService>();

            //увеличиваем таймаут бездействия
            //SendTimeout = new TimeSpan(7, 0, 0, 0),
            _netTcpBinding.ReceiveTimeout = new TimeSpan(7, 0, 0, 0);
            //OpenTimeout = new TimeSpan(7, 0, 0, 0),
            //CloseTimeout = new TimeSpan(7, 0, 0, 0)
        }

        public event Action StartActionInProgressEvent;

        public void Start()
        {
            this.StartActionInProgressEvent?.Invoke();

            Task.Run(
                () =>
                {
                    if (this.Connect() == false)
                        this.StopWaitRestart(); //очистить следы от предыдущего подключения, подождать и рестартануть
                }).Await();
        }

        private bool Connect()
        {
            //инициализация клиента сервиса
            EventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);

            //текущая сессия
            _appSessionId = Guid.NewGuid();

            var result = false;
            try
            {
                //подсоединяемся к сервису
                result = EventServiceHost.Connect(_appSessionId, _userId, _userRole);
            }
            catch
            {
                // ignored
            }

            if (result == true)
                PingHost(); //циклический пинг хоста

            return result;
        }

        public void Stop()
        {
            Task.Run(
                () =>
                {
                    #region not
                    //                    if (HostIsEnabled)
                    //                    {
                    //                        try
                    //                        {
                    //                            EventServiceHost.Disconnect(_appSessionId);
                    //                        }
                    //                        catch (TimeoutException e)
                    //                        {
                    //                            _container.Resolve<IMessageService>().Message(e.GetType().Name, e.PrintAllExceptions());
                    //                        }
                    //                        catch (CommunicationObjectFaultedException e)
                    //                        {
                    //                            _container.Resolve<IMessageService>().Message(e.GetType().Name, e.PrintAllExceptions());
                    //                        }
                    //#if DEBUG
                    //#else
                    //                        catch (Exception e)
                    //                        {
                    //                            _container.Resolve<IHvtAppLogger>().LogError("", e);
                    //                            _container.Resolve<IMessageService>().Message(e.GetType().Name, e.PrintAllExceptions());
                    //                        }
                    //#endif
                    //                    }
                    #endregion
                    if (HostIsEnabled) EventServiceHost.Disconnect(_appSessionId);

                })
                .Await(
                    errorCallback: e =>
                    {
                        _container.Resolve<IHvtAppLogger>().LogError(e.GetType().Name, e);
                        //_container.Resolve<IMessageService>().Message(e.GetType().Name, e.PrintAllExceptions());
                    },
                    lastAction: this.Disable
                );
        }

        /// <summary>
        /// Отключение от хоста
        /// </summary>
        private void Disable()
        {
            //сносим хост
            EventServiceHost?.Abort();
            EventServiceHost = null;
        }

        /// <summary>
        /// Очистить старое, подождать, рестартовать
        /// </summary>
        private void StopWaitRestart()
        {
            Stop();
            Action start = Start;
            start.SleepThenExecuteInAnotherThread(300);
        }

        /// <summary>
        /// Пинг хоста (циклический при удаче)
        /// </summary>
        private void PingHost()
        {
            Action pingAction = () =>
            {
                if (HostIsEnabled)
                {
                    try
                    {
                        if (EventServiceHost.HostIsAlive())
                        {
                            this.PingHost();
                            return;
                        }
                    }
                    catch
                    {
                        // ignored
                    }
                }

                this.StopWaitRestart();
            };

            pingAction.SleepThenExecuteInAnotherThread(300);
        }

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

        public bool SendNotification(NotificationUnit unit)
        {
            if (!this.HostIsEnabled) return false;

            var result = false;

            try
            {
                result = EventServiceHost.PriceEngineeringTaskNotificationEvent(
                    this._appSessionId,
                    unit.SenderUser.Id,
                    unit.RecipientUser.Id,
                    unit.RecipientRole,
                    unit.TargetEntityId,
                    $"{_notificationGeneratorService.GetTargetActionInfo(unit)}: {_notificationGeneratorService.GetTargetActionInfo(unit)}");
            }
            //хост недоступен
            catch (TimeoutException)
            {
                StopWaitRestart();
            }
            catch
            {
                StopWaitRestart();
            }

            return result;
        }
    }
}