using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
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
                case NotificationActionType.SavePriceCalculation:
                    break;
                case NotificationActionType.StartPriceCalculation:
                    break;
                case NotificationActionType.CancelPriceCalculation:
                    break;
                case NotificationActionType.RejectPriceCalculation:
                    break;
                case NotificationActionType.FinishPriceCalculation:
                    break;
                case NotificationActionType.SaveTechnicalRequrementsTask:
                    break;
                case NotificationActionType.StartTechnicalRequrementsTask:
                    break;
                case NotificationActionType.InstructTechnicalRequrementsTask:
                    break;
                case NotificationActionType.RejectTechnicalRequrementsTask:
                    break;
                case NotificationActionType.RejectByFrontManagerTechnicalRequrementsTask:
                    break;
                case NotificationActionType.FinishTechnicalRequrementsTask:
                    break;
                case NotificationActionType.AcceptTechnicalRequrementsTask:
                    break;
                case NotificationActionType.StopTechnicalRequrementsTask:
                    break;
                case NotificationActionType.SaveDirectumTask:
                    break;
                case NotificationActionType.StartDirectumTask:
                    break;
                case NotificationActionType.StopDirectumTask:
                    break;
                case NotificationActionType.PerformDirectumTask:
                    break;
                case NotificationActionType.AcceptDirectumTask:
                    break;
                case NotificationActionType.RejectDirectumTask:
                    break;
                case NotificationActionType.SaveIncomingRequest:
                    break;
                case NotificationActionType.SaveActualPayment:
                    break;
                case NotificationActionType.SavePaymentDocument:
                    break;


                case NotificationActionType.PriceEngineeringTasksStart:
                case NotificationActionType.PriceEngineeringTaskStart:
                case NotificationActionType.PriceEngineeringTaskStop:
                case NotificationActionType.PriceEngineeringTaskInstructToConstructor:
                case NotificationActionType.PriceEngineeringTaskFinish:
                case NotificationActionType.PriceEngineeringTaskAccept:
                case NotificationActionType.PriceEngineeringTaskRejectByManager:
                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                case NotificationActionType.PriceEngineeringTaskSendMessage:
                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                case NotificationActionType.PriceEngineeringTaskNotification:
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