using System;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.PriceEngineering.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceEngineeringTasks : NotificationHelper<PriceEngineeringTasks, AfterSavePriceEngineeringTasksEvent>
    {
        public NotificationHelperPriceEngineeringTasks(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationTextService notificationTextService) : 
            base(unitOfWork, unit, regionManager, eventAggregator, notificationTextService)
        {
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };

            switch (this.Unit.RecipientRole)
            {
                case Role.BackManager:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManager>(parameters);

                case Role.BackManagerBoss:
                    return () => RegionManager.RequestNavigateContentRegion<PriceEngineeringTasksViewBackManagerBoss>(parameters);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}