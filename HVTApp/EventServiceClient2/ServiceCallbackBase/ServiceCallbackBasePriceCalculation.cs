using System;
using EventServiceClient2.SyncEntities;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceCalculations.View;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace EventServiceClient2.ServiceCallbackBase
{
    public class ServiceCallbackBasePriceCalculation<TAfterPriceCalculationEvent>
        where TAfterPriceCalculationEvent : PubSubEvent<PriceCalculation>, new()
    {
        private readonly IUnityContainer _container;
        private readonly SyncContainer _syncContainer;

        public ServiceCallbackBasePriceCalculation(IUnityContainer container, SyncContainer syncContainer)
        {
            _container = container;
            _syncContainer = syncContainer;
        }

        public void Start(PriceCalculation priceCalculation, string message)
        {
            var action = new Action(() =>
            {
                _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), priceCalculation } });
            });

            _syncContainer.PublishWithinApp<PriceCalculation, TAfterPriceCalculationEvent>(priceCalculation);
            Popup.Popup.ShowPopup(message, $"{priceCalculation.Name} с Id {priceCalculation.Id}", action);
        }
    }
}