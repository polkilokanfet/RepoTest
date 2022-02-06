using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculation : SyncUnit<PriceCalculation, AfterSavePriceCalculationEvent>
    {
        public SyncPriceCalculation(IUnityContainer container) : base(container)
        {
        }

        protected override void DoPublishAction(PriceCalculation priceCalculation)
        {
            this.EventServiceHost.SavePriceCalculationPublishEvent(AppSessionId, priceCalculation.Id);
        }
    }
}