using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    public class NotificationGeneratorService : INotificationGeneratorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        public NotificationGeneratorService(IUnitOfWork unitOfWork, IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _unitOfWork = unitOfWork;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        private INotificationHelper GetNotification(NotificationUnit unit)
        {
            switch (unit.ActionType)
            {
                case NotificationActionType.StartPriceCalculation:
                case NotificationActionType.CancelPriceCalculation:
                case NotificationActionType.RejectPriceCalculation:
                case NotificationActionType.FinishPriceCalculation:
                    return new NotificationHelperPriceCalculation(_unitOfWork, unit, _regionManager, _eventAggregator);

                case NotificationActionType.StartTechnicalRequirementsTask:
                case NotificationActionType.InstructTechnicalRequirementsTask:
                case NotificationActionType.RejectTechnicalRequirementsTask:
                case NotificationActionType.RejectByFrontManagerTechnicalRequirementsTask:
                case NotificationActionType.FinishTechnicalRequirementsTask:
                case NotificationActionType.AcceptTechnicalRequirementsTask:
                case NotificationActionType.StopTechnicalRequirementsTask:
                    return new NotificationHelperTechnicalRequrementsTask(_unitOfWork, unit, _regionManager, _eventAggregator);

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
                    return new NotificationHelperPriceEngineeringTask(_unitOfWork, unit, _regionManager, _eventAggregator);

                case NotificationActionType.PriceEngineeringTasksStart:
                case NotificationActionType.PriceEngineeringTasksInstructToBackManager:
                    return new NotificationHelperPriceEngineeringTasks(_unitOfWork, unit, _regionManager, _eventAggregator);

                default:
                    throw new ArgumentOutOfRangeException();
            }
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

        public void RefreshTargetEntityAction(NotificationUnit unit)
        {
            this.GetNotification(unit).RefreshTargetEntityAction();
        }
    }
}