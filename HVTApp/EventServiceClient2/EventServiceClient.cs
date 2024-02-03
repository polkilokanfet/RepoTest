using System;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Threading.Tasks;
using EventServiceClient2.SyncEntities;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace EventServiceClient2
{
    public partial class EventServiceClient : IEventServiceClient, ISendNotificationThroughApp, EventServiceClient2.ServiceReference1.IEventServiceCallback
    {
        private readonly IUnityContainer _container;
        private readonly IPopupNotificationsService _popupNotificationsService;
        private readonly IFileManagerService _fileManagerService;
        private readonly IFilesStorageService _filesStorageService;
        private readonly Guid _userId = GlobalAppProperties.User.Id;
        private readonly Role _userRole = GlobalAppProperties.User.RoleCurrent;

        private readonly EndpointAddress _endpointAddress;
        private readonly NetTcpBinding _netTcpBinding;

        /// <summary>
        /// Хост сервиса подключен
        /// </summary>
        private bool HostIsEnabled => EventServiceHost != null && 
                                      EventServiceHost.State != CommunicationState.Faulted && 
                                      EventServiceHost.State != CommunicationState.Closed;

        private Guid _appSessionId;

        private ServiceReference1.EventServiceClient EventServiceHost { get; set; }

        private SyncContainer SyncContainer { get; }

        public EventServiceClient(IUnityContainer container)
        {
            _container = container;
            _popupNotificationsService = container.Resolve<IPopupNotificationsService>();
            _fileManagerService = container.Resolve<IFileManagerService>();
            _filesStorageService = container.Resolve<IFilesStorageService>();

            //увеличиваем таймаут бездействия
            _netTcpBinding = new NetTcpBinding(SecurityMode.None, true)
            {
                //SendTimeout = new TimeSpan(7, 0, 0, 0),
                ReceiveTimeout = new TimeSpan(7, 0, 0, 0),
                //OpenTimeout = new TimeSpan(7, 0, 0, 0),
                //CloseTimeout = new TimeSpan(7, 0, 0, 0)
            };

            _endpointAddress = new EndpointAddress(EventServiceAddresses.TcpBaseAddress);

            SyncContainer = new SyncContainer(_container);
            SyncContainer.ServiceHostIsDisabled += StopWaitRestart;
        }

        public void Start()
        {
            Task.Run(
                () =>
                {
                    try
                    {
                        CheckMessagesInDb();
                    }
                    catch (Exception)
                    {
                    }

                    try
                    {

                        //проверка на то стартован ли уже сервис
                        if (HostIsEnabled)
                            return;

                        //инициализация клиента сервиса
                        EventServiceHost = new ServiceReference1.EventServiceClient(new InstanceContext(this), _netTcpBinding, _endpointAddress);

                        //текущая сессия
                        _appSessionId = Guid.NewGuid();

                        //коннектимся к сервису
                        if (EventServiceHost.Connect(_appSessionId, _userId, _userRole))
                        {
                            //Подключение контейнера синхронизации
                            SyncContainer.Connect(EventServiceHost, _appSessionId);

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
                    () => this.Disable(),
                    e =>
                    {
                        _container.Resolve<IHvtAppLogger>().LogError(e.GetType().Name, e);
                        //_container.Resolve<IMessageService>().Message(e.GetType().Name, e.PrintAllExceptions());
                        this.Disable();
                    }
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

            //освобождаем контейнер синхронизации
            SyncContainer.Disconnect();
        }

        /// <summary>
        /// Очистить старое, подождать, рестартовать
        /// </summary>
        private void StopWaitRestart()
        {
            Stop();
            SleepInvokeInOtherThread(this.Start, 600);
        }

        /// <summary>
        /// Создать новый поток, уснуть, запустить действие в этом новом потоке
        /// </summary>
        /// <param name="action">Действие</param>
        /// <param name="seconds">На сколько секунд уснуть</param>
        private void SleepInvokeInOtherThread(Action action, int seconds)
        {
            Task.Run(
                () =>
                {
                    Thread.Sleep(new TimeSpan(0, 0, 0, seconds));
                    action.Invoke();
                }).Await();
        }

        /// <summary>
        /// Пинг хоста (циклический при удаче)
        /// </summary>
        private void PingHost()
        {
            Task.Run(
                () =>
                {
                    if (HostIsEnabled)
                    {
                        try
                        {
                            if (EventServiceHost.HostIsAlive())
                            {
                                Thread.Sleep(new TimeSpan(0, 0, 5, 0));
                                this.PingHost();
                            }
                        }
                        catch (Exception)
                        {
                            this.StopWaitRestart();
                        }
                    }
                }).Await();
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
                }

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
                    case EventServiceActionType.SaveActualPayment:
                    {
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTasksStart:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTasksStartServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskStart:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskStartServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskStop:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskStopServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskInstruct:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskInstructServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskFinish:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskFinishServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskAccept:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskAcceptServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskRejectByManager:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskRejectByManagerServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskRejectByConstructor:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskRejectByConstructorServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskSendMessage:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskSendMessageServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskFinishGoToVerification:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskFinishGoToVerificationServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskVerificationRejectedByHead:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskVerificationRejectedByHeadServiceCallback);
                        break;
                    }
                    case EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                    {
                        this.CheckMessageInDbAction(unit, unitOfWork, OnPriceEngineeringTaskVerificationAcceptedByHeadServiceCallback);
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

        public bool SendNotification(NotificationAboutPriceEngineeringTaskEventArg item)
        {
            bool notificationSent = false;

            if (!this.HostIsEnabled) return false;

            try
            {
                notificationSent = EventServiceHost.PriceEngineeringTaskNotificationEvent(
                    this._appSessionId,
                    GlobalAppProperties.User.Id,
                    item.RecipientUser.Id,
                    item.RecipientRole,
                    item.PriceEngineeringTask.Id,
                    $"{item.Message}: {item.PriceEngineeringTask}");
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

    public partial class EventServiceClient : IEventServiceClient
    {
        public bool SaveDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        {
            return this.EventServiceHost.SaveDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        }

        public bool StartDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        {
            return this.EventServiceHost.StartDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        }

        public bool StopDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        {
            return this.EventServiceHost.StopDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        }

        public bool PerformDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        {
            return this.EventServiceHost.PerformDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        }

        public bool AcceptDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        {
            return this.EventServiceHost.AcceptDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        }

        public bool RejectDirectumTaskPublishEvent(Guid targetUserId, Role targetRole, Guid taskId)
        {
            return this.EventServiceHost.RejectDirectumTaskPublishEvent(_appSessionId, targetUserId, targetRole, taskId);
        }

        public bool SavePriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return this.EventServiceHost.SavePriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        }

        public bool StartPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return this.EventServiceHost.StartPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        }

        public bool FinishPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return this.EventServiceHost.FinishPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        }

        public bool CancelPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return this.EventServiceHost.CancelPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        }

        public bool RejectPriceCalculationPublishEvent(Guid targetUserId, Role targetRole, Guid priceCalculationId)
        {
            return this.EventServiceHost.RejectPriceCalculationPublishEvent(_appSessionId, targetUserId, targetRole, priceCalculationId);
        }

        public bool SaveIncomingRequestPublishEvent(Guid targetUserId, Role targetRole, Guid requestId)
        {
            return this.EventServiceHost.SaveIncomingRequestPublishEvent(_appSessionId, targetUserId, targetRole, requestId);
        }

        public bool SaveIncomingDocumentPublishEvent(Guid targetUserId, Role targetRole, Guid documentId)
        {
            return this.EventServiceHost.SaveIncomingDocumentPublishEvent(_appSessionId, targetUserId, targetRole, documentId);
        }

        public bool SaveTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.SaveTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool StartTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.StartTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool InstructTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole,
            Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.InstructTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool StopTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.StopTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool RejectTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.RejectTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.RejectByFrontManagerTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool FinishTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.FinishTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool AcceptTechnicalRequarementsTaskPublishEvent(Guid targetUserId, Role targetRole, Guid technicalRequarementsTaskId)
        {
            return this.EventServiceHost.AcceptTechnicalRequarementsTaskPublishEvent(_appSessionId, targetUserId, targetRole, technicalRequarementsTaskId);
        }

        public bool SavePaymentDocumentPublishEvent(Guid targetUserId, Role targetRole, Guid paymentDocumentId)
        {
            return this.EventServiceHost.SavePaymentDocumentPublishEvent(_appSessionId, targetUserId, targetRole, paymentDocumentId);
        }

        public bool PriceEngineeringTaskNotificationEvent(Guid eventSourceAppSessionId, Guid userAuthorId, Guid userTargetId, Role userTargetRole, Guid priceEngineeringTaskId, string message)
        {
            return this.EventServiceHost.PriceEngineeringTaskNotificationEvent(_appSessionId, userAuthorId, userTargetId, userTargetRole, priceEngineeringTaskId, message);
        }

        public bool PriceEngineeringTasksStartPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTasksId)
        {
            return this.EventServiceHost.PriceEngineeringTasksStartPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTasksId);
        }

        public bool PriceEngineeringTaskStartPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskStartPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskInstructPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskInstructPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskFinishPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskFinishPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskFinishGoToVerificationPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskFinishGoToVerificationPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskVerificationRejectedByHeadPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskVerificationRejectedByHeadPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskVerificationAcceptedByHeadPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskVerificationAcceptedByHeadPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskAcceptPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskAcceptPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskRejectByManagerPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskRejectByManagerPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskRejectByConstructorPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskRejectByConstructorPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskStopPublishEvent(Guid targetUserId, Role targetRole, Guid priceEngineeringTaskId)
        {
            return this.EventServiceHost.PriceEngineeringTaskStopPublishEvent(_appSessionId, targetUserId, targetRole, priceEngineeringTaskId);
        }

        public bool PriceEngineeringTaskSendMessagePublishEvent(Guid targetUserId, Role targetRole, Guid messageId)
        {
            return this.EventServiceHost.PriceEngineeringTaskSendMessagePublishEvent(_appSessionId, targetUserId, targetRole, messageId);
        }
    }
}