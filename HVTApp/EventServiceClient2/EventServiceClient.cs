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
        private readonly IUnityContainer _container;
        private readonly INotificationFromDataBaseService _notificationFromDataBaseService;
        private readonly INotificationGeneratorService _notificationGeneratorService;
        private readonly IPopupNotificationsService _popupNotificationsService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IFilesStorageService _filesStorageService;
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
            _notificationFromDataBaseService = container.Resolve<INotificationFromDataBaseService>();
            _notificationGeneratorService = container.Resolve<INotificationGeneratorService>();
            _popupNotificationsService = container.Resolve<IPopupNotificationsService>();
            _fileManagerService = container.Resolve<IFileManagerService>();
            _filesStorageService = container.Resolve<IFilesStorageService>();

            //увеличиваем таймаут бездействия
            //SendTimeout = new TimeSpan(7, 0, 0, 0),
            _netTcpBinding.ReceiveTimeout = new TimeSpan(7, 0, 0, 0);
            //OpenTimeout = new TimeSpan(7, 0, 0, 0),
            //CloseTimeout = new TimeSpan(7, 0, 0, 0)
        }

        public void Start()
        {
            Task.Run(
                () =>
                {
                    Task.Run(() => _notificationFromDataBaseService.CheckMessagesInDbAndShowNotifications()).Await();

                    try
                    {
                        //проверка на то стартован ли уже сервис и доступен ли он
                        if (HostIsEnabled && EventServiceHost.HostIsAlive())
                            return;

                        //инициализация клиента сервиса
                        EventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);

                        //текущая сессия
                        _appSessionId = Guid.NewGuid();

                        //коннектимся к сервису
                        if (EventServiceHost.Connect(_appSessionId, _userId, _userRole))
                        {
                            //циклический пинг хоста
                            PingHost();
                        }
                        else
                        {
                            this._container.Resolve<IHvtAppLogger>().LogError("", new Exception("_eventServiceClient.Connect() вернул false. Это приложение уже подключено к сервису."));
                            //throw new Exception("_eventServiceClient.Connect() вернул false");
                        }
                    }
                    catch (Exception)
                    {
                        //очистить следы от предыдущего подключения, подождать и рестартануть
                        this.StopWaitRestart();
                    }
                }).Await();
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
            Action a = () =>
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
                    }
                }

                this.StopWaitRestart();
            };

            a.SleepThenExecuteInAnotherThread(300);
        }

        public void CopyProjectAttachmentsRequest(Guid userId, Guid projectId, string targetDirectory)
        {
            if (HostIsEnabled)
            {
                EventServiceHost.CopyProjectAttachments(userId, projectId, targetDirectory);
            }
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

        public void CheckMessagesInDb()
        {
            var unitOfWork = _container.Resolve<IUnitOfWork>();
            
            //Есть ли в базе данных сообщения для текущего пользователя?
            var units = unitOfWork.Repository<EventServiceUnit>().Find(unit => unit.User.Id == GlobalAppProperties.User.Id);

            foreach (var unit in units)
            {
                if (unit.EventServiceActionType == EventServiceActionType.PriceEngineeringTaskNotification)
                {
                    if (unit.Role.HasValue && GlobalAppProperties.User.RoleCurrent == unit.Role)
                    {
                        try
                        {
                            var result = OnPriceEngineeringNotificationServiceCallback(unit.TargetEntityId, unit.Message);
                            if (result == true)
                            {
                                unitOfWork.Repository<EventServiceUnit>().Delete(unit);
                            }
                        }
                        catch (ArgumentNullException)
                        {
                            unitOfWork.Repository<EventServiceUnit>().Delete(unit);
                        }
                        catch
                        {
                        }
                    }

                    continue;
                }

                if (unit.Role.HasValue == false || GlobalAppProperties.User.RoleCurrent != unit.Role)
                    continue;

                switch (unit.EventServiceActionType)
            {
                case EventServiceActionType.SavePriceCalculation:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnSavePriceCalculationServiceCallback);
                    break;
                }
                //старт расчета ПЗ
                case EventServiceActionType.StartPriceCalculation:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnStartPriceCalculationServiceCallback);
                    break;
                }
                case EventServiceActionType.CancelPriceCalculation:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnCancelPriceCalculationServiceCallback);
                    break;
                }
                case EventServiceActionType.RejectPriceCalculation:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnRejectPriceCalculationServiceCallback);
                    break;
                }
                case EventServiceActionType.FinishPriceCalculation:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnFinishPriceCalculationServiceCallback);
                    break;
                }

                case EventServiceActionType.SaveTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnSaveTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.StartTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnStartTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                //поручение расчета ПЗ
                case EventServiceActionType.InstructTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnInstructTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.RejectTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnRejectTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.RejectByFrontManagerTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.FinishTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnFinishTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.AcceptTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnAcceptTechnicalRequarementsTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.StopTechnicalRequrementsTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnStopTechnicalRequarementsTaskServiceCallback);
                    break;
                }

                case EventServiceActionType.SaveDirectumTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnSaveDirectumTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.StartDirectumTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnStartDirectumTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.StopDirectumTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnStopDirectumTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.PerformDirectumTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnPerformDirectumTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.AcceptDirectumTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnAcceptDirectumTaskServiceCallback);
                    break;
                }
                case EventServiceActionType.RejectDirectumTask:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnRejectDirectumTaskServiceCallback);
                    break;
                }

                case EventServiceActionType.SaveIncomingRequest:
                {
                    this.CheckMessageInDbAction(unit, unitOfWork, OnSaveIncomingRequestServiceCallback);
                    break;
                }
            }
            }

            unitOfWork.SaveChanges();
            unitOfWork.Dispose();
        }

        private void CheckMessageInDbAction(EventServiceUnit unit, IUnitOfWork unitOfWork, Func<Guid, bool> callback)
        {
            try
            {
                var result = callback(unit.TargetEntityId);
                if (result == true)
                {
                    unitOfWork.Repository<EventServiceUnit>().Delete(unit);
                }
            }
            catch (ArgumentNullException)
            {
                unitOfWork.Repository<EventServiceUnit>().Delete(unit);
            }
            catch
            {
            }
        }

        public bool SendNotification(NotificationUnit unit)
        {
            bool notificationSent = false;

            if (!this.HostIsEnabled) return false;

            try
            {
                notificationSent = EventServiceHost.PriceEngineeringTaskNotificationEvent(
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

            return notificationSent;
        }
    }
}