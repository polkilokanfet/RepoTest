using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperTechnicalRequrementsTask : NotificationHelper<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public NotificationHelperTechnicalRequrementsTask(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator) : 
            base(unitOfWork, unit, regionManager, eventAggregator)
        {
        }

        public override string GetCommonInfo()
        {
            return this.TargetUnit.ToString();
        }

        public override string GetActionInfo()
        {
            switch (Unit.ActionType)
            {
                case NotificationActionType.SaveTechnicalRequirementsTask:
                    return "задача ТСЕ";
                case NotificationActionType.StartTechnicalRequirementsTask:
                    return "Запущена задача ТСЕ";
                case NotificationActionType.InstructTechnicalRequirementsTask:
                    return "Поручена задача ТСЕ";
                case NotificationActionType.RejectTechnicalRequirementsTask:
                    return "Отклонена задача ТСЕ";
                case NotificationActionType.RejectByFrontManagerTechnicalRequirementsTask:
                    return "Отклонена задача ТСЕ";
                case NotificationActionType.FinishTechnicalRequirementsTask:
                    return "Завершена задача ТСЕ";
                case NotificationActionType.AcceptTechnicalRequirementsTask:
                    return "Принята задача ТСЕ";
                case NotificationActionType.StopTechnicalRequirementsTask:
                    return "Остановлена задача ТСЕ";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };
            return () => RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(new NavigationParameters { { nameof(PriceCalculation), parameters } });
        }
    }
}