using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Regions;

namespace NotificationsService
{
    public class NotificationGeneratorService : INotificationGeneratorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;

        public NotificationGeneratorService(IUnitOfWork unitOfWork, IRegionManager regionManager)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
        }

        private INotificationHelper GetNotification(NotificationUnit unit)
        {
            throw new NotImplementedException();
            switch (unit.ActionType)
            {
                case EventServiceActionType.SavePriceCalculation:
                    break;
                case EventServiceActionType.StartPriceCalculation:
                    break;
                case EventServiceActionType.CancelPriceCalculation:
                    break;
                case EventServiceActionType.RejectPriceCalculation:
                    break;
                case EventServiceActionType.FinishPriceCalculation:
                    break;
                case EventServiceActionType.SaveTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.StartTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.InstructTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.RejectTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.RejectByFrontManagerTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.FinishTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.AcceptTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.StopTechnicalRequrementsTask:
                    break;
                case EventServiceActionType.SaveDirectumTask:
                    break;
                case EventServiceActionType.StartDirectumTask:
                    break;
                case EventServiceActionType.StopDirectumTask:
                    break;
                case EventServiceActionType.PerformDirectumTask:
                    break;
                case EventServiceActionType.AcceptDirectumTask:
                    break;
                case EventServiceActionType.RejectDirectumTask:
                    break;
                case EventServiceActionType.SaveIncomingRequest:
                    break;
                case EventServiceActionType.SaveActualPayment:
                    break;
                case EventServiceActionType.SavePaymentDocument:
                    break;


                case EventServiceActionType.PriceEngineeringTasksStart:
                case EventServiceActionType.PriceEngineeringTaskStart:
                case EventServiceActionType.PriceEngineeringTaskStop:
                case EventServiceActionType.PriceEngineeringTaskInstructToConstructor:
                case EventServiceActionType.PriceEngineeringTaskFinish:
                case EventServiceActionType.PriceEngineeringTaskAccept:
                case EventServiceActionType.PriceEngineeringTaskRejectByManager:
                case EventServiceActionType.PriceEngineeringTaskRejectByConstructorToManager:
                case EventServiceActionType.PriceEngineeringTaskSendMessage:
                case EventServiceActionType.PriceEngineeringTaskFinishGoToVerification:
                case EventServiceActionType.PriceEngineeringTaskVerificationRejectedByHead:
                case EventServiceActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                case EventServiceActionType.PriceEngineeringTaskNotification:
                    return new NotificationPriceEngineeringTask(_unitOfWork, unit, _regionManager);


                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        public string GetTargetEntityInfo(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetTargetEntityInfo();
        }

        public string GetTargetActionInfo(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetTargetActionInfo();
        }

        public Action GetOpenTargetEntityViewAction(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetOpenTargetEntityViewAction();
        }

    }
}