using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceEngineeringTask : NotificationHelper<PriceEngineeringTask, AfterSavePriceEngineeringTaskEvent>
    {
        public NotificationHelperPriceEngineeringTask(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator) : 
            base(unitOfWork, unit, regionManager,eventAggregator)
        {
        }

        public override string GetCommonInfo()
        {
            PriceEngineeringTasks tasks = TargetUnit.GetPriceEngineeringTasks(UnitOfWork);
            PriceEngineeringTask taskTop = TargetUnit.GetTopPriceEngineeringTask(UnitOfWork);

            return this.Unit.GetCommonInfo(tasks, TargetUnit, taskTop);
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