using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.TechnicalRequrementsTasksModule;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperTechnicalRequrementsTask : NotificationHelper<TechnicalRequrementsTask, AfterSaveTechnicalRequrementsTaskEvent>
    {
        public NotificationHelperTechnicalRequrementsTask(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationTextService notificationTextService) : 
            base(unitOfWork, unit, regionManager, eventAggregator, notificationTextService)
        {
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };
            return () => RegionManager.RequestNavigateContentRegion<TechnicalRequrementsTaskView>(parameters);
        }
    }
}