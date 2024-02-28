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
                case NotificationActionType.SaveTechnicalRequrementsTask:
                    return "задача ТСЕ";
                case NotificationActionType.StartTechnicalRequrementsTask:
                    return "Запущена задача ТСЕ";
                case NotificationActionType.InstructTechnicalRequrementsTask:
                    return "Поручена задача ТСЕ";
                case NotificationActionType.RejectTechnicalRequrementsTask:
                    return "Отклонена задача ТСЕ";
                case NotificationActionType.RejectByFrontManagerTechnicalRequrementsTask:
                    return "Отклонена задача ТСЕ";
                case NotificationActionType.FinishTechnicalRequrementsTask:
                    return "Завершена задача ТСЕ";
                case NotificationActionType.AcceptTechnicalRequrementsTask:
                    return "Принята задача ТСЕ";
                case NotificationActionType.StopTechnicalRequrementsTask:
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