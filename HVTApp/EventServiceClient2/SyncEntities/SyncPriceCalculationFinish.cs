using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculationFinish : Sync<PriceCalculation, AfterFinishPriceCalculationEvent>
    {
        public SyncPriceCalculationFinish(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceHost, Guid appSessionId) : base(container, eventServiceHost, appSessionId)
        {
        }

        protected override Action<PriceCalculation> PublishEventAction
        {
            get { return priceCalculation => EventServiceHost.FinishPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id); }
        }
    }
}