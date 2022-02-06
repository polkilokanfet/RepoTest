using System;
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
        private readonly IEventAggregator _eventAggregator;

        public ServiceCallbackBasePriceCalculation(IUnityContainer container)
        {
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();
        }

        public void Start(PriceCalculation priceCalculation, string message)
        {
            var action = new Action(() =>
            {
                _container.Resolve<IRegionManager>().RequestNavigateContentRegion<PriceCalculationView>(new NavigationParameters { { nameof(PriceCalculation), priceCalculation } });
            });

            _eventAggregator.GetEvent<TAfterPriceCalculationEvent>().Publish(priceCalculation);
            Popup.Popup.ShowPopup(message, $"{priceCalculation.Name} с Id {priceCalculation.Id}", action);
        }
    }
}