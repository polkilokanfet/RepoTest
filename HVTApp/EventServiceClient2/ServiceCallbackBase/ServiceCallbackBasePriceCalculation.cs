using System;
using System.Linq;
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
        where TAfterPriceCalculationEvent : PubSubEvent<PriceCalculation>
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

            var facilities = priceCalculation.PriceCalculationItems
                .SelectMany(priceCalculationItem => priceCalculationItem.SalesUnits)
                .Select(salesUnit => salesUnit.Facility.ToString())
                .Distinct().ToStringEnum(",");

            _syncContainer.Publish<PriceCalculation, TAfterPriceCalculationEvent>(priceCalculation);
            Popup.Popup.ShowPopup(message, $"Расчет ПЗ для {facilities} с Id {priceCalculation.Id}", action);
        }
    }
}