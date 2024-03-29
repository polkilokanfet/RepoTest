using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.PriceEngineering.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceEngineeringTask : NotificationHelper<PriceEngineeringTask, AfterSavePriceEngineeringTaskEvent>
    {
        public NotificationHelperPriceEngineeringTask(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationTextService notificationTextService) : 
            base(unitOfWork, unit, regionManager, eventAggregator, notificationTextService)
        {
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };

            switch (this.Unit.RecipientRole)
            {
                case Role.SalesManager:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewManager>(parameters);

                case Role.Constructor:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewConstructor>(parameters);

                case Role.BackManager:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);

                case Role.BackManagerBoss:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManagerBoss>(parameters);

                case Role.DesignDepartmentHead:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewDesignDepartmentHead>(parameters);

                case Role.PlanMaker:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewPlanMaker>(parameters);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}