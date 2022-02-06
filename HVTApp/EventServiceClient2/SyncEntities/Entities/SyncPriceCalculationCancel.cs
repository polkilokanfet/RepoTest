using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculationCancel : SyncUnit<PriceCalculation, AfterCancelPriceCalculationEvent>
    {
        public SyncPriceCalculationCancel(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(PriceCalculation priceCalculation)
        {
            EventServiceHost.CancelPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id);
        }
    }

    public class SyncPriceCalculationReject : SyncUnit<PriceCalculation, AfterRejectPriceCalculationEvent>
    {
        public SyncPriceCalculationReject(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(PriceCalculation priceCalculation)
        {
            EventServiceHost.RejectPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id);
        }
    }

}
