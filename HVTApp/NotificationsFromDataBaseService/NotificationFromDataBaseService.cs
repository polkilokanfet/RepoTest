using System;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;

namespace NotificationsFromDataBaseService
{
    public class NotificationFromDataBaseService : INotificationFromDataBaseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPopupNotificationsService _popupNotificationsService;

        public NotificationFromDataBaseService(IUnitOfWork unitOfWork, IPopupNotificationsService popupNotificationsService)
        {
            _unitOfWork = unitOfWork;
            _popupNotificationsService = popupNotificationsService;
        }

        /// <summary>
        /// Сохранение уведомления в базе данных
        /// </summary>
        /// <param name="notification"></param>
        public void SaveNotificationInDataBase(NotificationAboutPriceEngineeringTaskEventArg notification)
        {
            var unit = new EventServiceUnit
            {
                User = _unitOfWork.Repository<User>().GetById(notification.RecipientUser.Id),
                Role = notification.RecipientRole,
                Message = $"{notification.Message}: {notification.PriceEngineeringTask}",
                TargetEntityId = notification.PriceEngineeringTask.Id,
                EventServiceActionType = EventServiceActionType.PriceEngineeringTaskNotification
            };
            _unitOfWork.SaveEntity(unit);
        }

        public void SaveNotificationInDataBase(EventServiceUnit unit)
        {
            unit.User = _unitOfWork.Repository<User>().GetById(unit.User.Id);
            _unitOfWork.SaveEntity(unit);
        }


        //public void CheckMessagesInDb()
        //{
        //    //Есть ли в базе данных сообщения для текущего пользователя?
        //    var eventServiceUnits = _unitOfWork.Repository<EventServiceUnit>().Find(unit => unit.User.Id == GlobalAppProperties.User.Id);

        //    foreach (var unit in eventServiceUnits)
        //    {
        //        if (unit.EventServiceActionType == EventServiceActionType.PriceEngineeringTaskNotification)
        //        {
        //            if (unit.Role.HasValue && GlobalAppProperties.User.RoleCurrent == unit.Role)
        //            {
        //                try
        //                {
        //                    var result = NotificationPriceEngineeringTask(unit.TargetEntityId, unit.Message);
        //                    if (result == true)
        //                    {
        //                        _unitOfWork.Repository<EventServiceUnit>().Delete(unit);
        //                    }
        //                }
        //                catch (ArgumentNullException)
        //                {
        //                    _unitOfWork.Repository<EventServiceUnit>().Delete(unit);
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }

        //        switch (unit.EventServiceActionType)
        //        {
        //            case EventServiceActionType.SavePriceCalculation:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnSavePriceCalculationServiceCallback);
        //                    break;
        //                }
        //            //старт расчета ПЗ
        //            case EventServiceActionType.StartPriceCalculation:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnStartPriceCalculationServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.CancelPriceCalculation:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnCancelPriceCalculationServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.RejectPriceCalculation:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnRejectPriceCalculationServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.FinishPriceCalculation:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnFinishPriceCalculationServiceCallback);
        //                    break;
        //                }

        //            case EventServiceActionType.SaveTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnSaveTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.StartTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnStartTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            //поручение расчета ПЗ
        //            case EventServiceActionType.InstructTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnInstructTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.RejectTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnRejectTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.RejectByFrontManagerTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnRejectByFrontManagerTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.FinishTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnFinishTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.AcceptTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnAcceptTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.StopTechnicalRequrementsTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnStopTechnicalRequarementsTaskServiceCallback);
        //                    break;
        //                }

        //            case EventServiceActionType.SaveDirectumTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnSaveDirectumTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.StartDirectumTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnStartDirectumTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.StopDirectumTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnStopDirectumTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PerformDirectumTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPerformDirectumTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.AcceptDirectumTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnAcceptDirectumTaskServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.RejectDirectumTask:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnRejectDirectumTaskServiceCallback);
        //                    break;
        //                }

        //            case EventServiceActionType.SaveIncomingRequest:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnSaveIncomingRequestServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.SaveActualPayment:
        //                {
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTasksStart:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTasksStartServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskStart:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskStartServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskStop:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskStopServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskInstruct:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskInstructServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskFinish:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskFinishServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskAccept:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskAcceptServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskRejectByManager:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskRejectByManagerServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskRejectByConstructor:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskRejectByConstructorServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskSendMessage:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskSendMessageServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskFinishGoToVerification:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskFinishGoToVerificationServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskVerificationRejectedByHead:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskVerificationRejectedByHeadServiceCallback);
        //                    break;
        //                }
        //            case EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead:
        //                {
        //                    this.CheckMessageInDbAction(unit, _unitOfWork, OnPriceEngineeringTaskVerificationAcceptedByHeadServiceCallback);
        //                    break;
        //                }
        //        }
        //    }

        //    _unitOfWork.SaveChanges();
        //}

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

        public bool NotificationPriceEngineeringTask(Guid priceEngineeringTaskId, string message)
        {
            var priceEngineeringTask = _unitOfWork.Repository<PriceEngineeringTask>().GetById(priceEngineeringTaskId);
            var title = $"{priceEngineeringTask} с Id {priceEngineeringTask.Id}";
            _popupNotificationsService.ShowPopupNotification(priceEngineeringTask, message, title);
            return true;
        }
    }
}
