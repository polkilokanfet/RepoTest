﻿using System;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.PriceCalculations.View;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperPriceCalculation : NotificationHelper<PriceCalculation, AfterSavePriceCalculationEvent>
    {
        public NotificationHelperPriceCalculation(
            IUnitOfWork unitOfWork, 
            NotificationUnit unit, 
            IRegionManager regionManager, 
            IEventAggregator eventAggregator, INotificationTextService notificationTextService) : 
            base(unitOfWork, unit, regionManager, eventAggregator, notificationTextService)
        {
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };
            return () => RegionManager.RequestNavigateContentRegion<PriceCalculationView>(parameters);
        }
    }
}