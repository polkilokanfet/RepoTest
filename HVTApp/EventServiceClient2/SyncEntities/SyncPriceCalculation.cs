using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculation : Sync<PriceCalculation, AfterSavePriceCalculationEvent>
    {
        public SyncPriceCalculation(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<PriceCalculation> PublishEventAction
        {
            get { return priceCalculation => this.EventServiceClient.SavePriceCalculationPublishEvent(AppSessionId, priceCalculation.Id); }
        }
    }
}