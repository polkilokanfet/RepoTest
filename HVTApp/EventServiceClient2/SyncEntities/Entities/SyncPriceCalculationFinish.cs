using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculationFinish : SyncUnit<PriceCalculation, AfterFinishPriceCalculationEvent>
    {
        public SyncPriceCalculationFinish(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(PriceCalculation priceCalculation)
        {
            EventServiceHost.FinishPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id);
        }
    }
}