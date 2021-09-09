using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculation : SyncUnit<PriceCalculation, AfterSavePriceCalculationEvent>
    {
        public SyncPriceCalculation(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<PriceCalculation> PublishEventAction
        {
            get { return priceCalculation => this.EventServiceHost.SavePriceCalculationPublishEvent(AppSessionId, priceCalculation.Id); }
        }
    }
}