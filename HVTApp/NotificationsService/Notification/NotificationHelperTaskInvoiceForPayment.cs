using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManager;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss;
using HVTApp.UI.TaskInvoiceForPayment1.ForManager;
using HVTApp.UI.TaskInvoiceForPayment1.ForPlanMaker;
using Prism.Events;
using Prism.Regions;

namespace NotificationsService
{
    internal class NotificationHelperTaskInvoiceForPayment : NotificationHelper<TaskInvoiceForPayment, AfterSaveTaskInvoiceForPaymentEvent>
    {
        public NotificationHelperTaskInvoiceForPayment(IUnitOfWork unitOfWork, NotificationUnit unit, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationTextService notificationTextService) : base(unitOfWork, unit, regionManager, eventAggregator, notificationTextService)
        {
        }

        public override Action GetOpenTargetEntityViewAction()
        {
            var parameters = new NavigationParameters { { string.Empty, this.TargetUnit } };

            switch (this.Unit.RecipientRole)
            {
                case Role.SalesManager:
                    return () => RegionManager.RequestNavigateContentRegion<TaskInvoiceForPaymentManagerView>(parameters);

                case Role.BackManager:
                    return () => RegionManager.RequestNavigateContentRegion<TaskInvoiceForPaymentBackManagerView>(parameters);

                case Role.BackManagerBoss:
                    return () => RegionManager.RequestNavigateContentRegion<TaskInvoiceForPaymentBackManagerBossView>(parameters);

                case Role.PlanMaker:
                    return () => RegionManager.RequestNavigateContentRegion<TaskInvoiceForPaymentPlanMakerView>(parameters);

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}