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
            switch (unit.ActionType)
            {
                case NotificationActionType.SavePriceCalculation:
                case NotificationActionType.StartPriceCalculation:
                case NotificationActionType.CancelPriceCalculation:
                case NotificationActionType.RejectPriceCalculation:
                case NotificationActionType.FinishPriceCalculation:
                    return new NotificationPriceCalculation(_unitOfWork, unit, _regionManager);

                case NotificationActionType.SaveTechnicalRequrementsTask:
                case NotificationActionType.StartTechnicalRequrementsTask:
                case NotificationActionType.InstructTechnicalRequrementsTask:
                case NotificationActionType.RejectTechnicalRequrementsTask:
                case NotificationActionType.RejectByFrontManagerTechnicalRequrementsTask:
                case NotificationActionType.FinishTechnicalRequrementsTask:
                case NotificationActionType.AcceptTechnicalRequrementsTask:
                case NotificationActionType.StopTechnicalRequrementsTask:
                    return new NotificationTechnicalRequrementsTask(_unitOfWork, unit, _regionManager);


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
                case NotificationActionType.PriceEngineeringTaskRejectByHeadToManager:
                case NotificationActionType.PriceEngineeringTaskRejectByConstructorToManager:
                case NotificationActionType.PriceEngineeringTaskSendMessage:
                case NotificationActionType.PriceEngineeringTaskFinishGoToVerification:
                case NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead:
                case NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead:
                case NotificationActionType.PriceEngineeringTaskInstructToPlanMaker:
                case NotificationActionType.PriceEngineeringTaskLoadToTceStart:
                case NotificationActionType.PriceEngineeringTaskLoadToTceFinish:
                case NotificationActionType.PriceEngineeringTaskProductionRequestStart:
                case NotificationActionType.PriceEngineeringTaskProductionRequestFinish:
                case NotificationActionType.PriceEngineeringTaskProductionRequestStop:
                case NotificationActionType.PriceEngineeringTaskProductionRequestStopConfirm:
                case NotificationActionType.PriceEngineeringTaskProductionRequestStopReject:
                    return new NotificationPriceEngineeringTask(_unitOfWork, unit, _regionManager);

                case NotificationActionType.PriceEngineeringTasksInstructToBackManager:
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }

            throw new NotImplementedException();
        }

        public string GetCommonInfo(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetCommonInfo();
        }

        public string GetActionInfo(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetActionInfo();
        }

        public Action GetOpenTargetEntityViewAction(NotificationUnit unit)
        {
            return this.GetNotification(unit).GetOpenTargetEntityViewAction();
        }

    }
}