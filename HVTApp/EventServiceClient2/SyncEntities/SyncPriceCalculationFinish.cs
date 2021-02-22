using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace EventServiceClient2.SyncEntities
{
    public class SyncPriceCalculationFinish : Sync<PriceCalculation, AfterFinishPriceCalculationEvent>
    {
        public SyncPriceCalculationFinish(IUnityContainer container, ServiceReference1.EventServiceClient eventServiceClient, Guid appSessionId) : base(container, eventServiceClient, appSessionId)
        {
        }

        protected override Action<PriceCalculation> PublishEventAction
        {
            get { return priceCalculation => EventServiceClient.FinishPriceCalculationPublishEvent(AppSessionId, priceCalculation.Id); }
        }
    }
}