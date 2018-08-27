using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        public OfferLookupListViewModel OfferListViewModel { get; }
        public UnitLookupListViewModel UnitLookupListViewModel { get; }

        public Market2ViewModel(IUnityContainer container, OfferLookupListViewModel offerListViewModel, 
            UnitLookupListViewModel unitLookupListViewModel, OfferLookupDataService offerLookupDataService) : base(container)
        {
            OfferListViewModel = offerListViewModel;
            UnitLookupListViewModel = unitLookupListViewModel;

            this.SelectedLookupChanged += async project =>
            {
                UnitLookupListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
                foreach (var offerLookup in project.Offers)
                {
                    if (offerLookup.OfferUnits == null)
                        offerLookup.OfferUnits = (await offerLookupDataService.GetLookupById(offerLookup.Id)).OfferUnits.ToList();
                }
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                UnitLookupListViewModel.Load(offer.OfferUnits);
            };

        }
    }
}
