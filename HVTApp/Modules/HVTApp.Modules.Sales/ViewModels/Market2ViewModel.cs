using System.Collections.Generic;
using System.Linq;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel
    {
        public ProjectLookupListViewModel ProjectListViewModel { get; }
        public OfferLookupListViewModel OfferListViewModel { get; }
        public UnitLookupListViewModel UnitLookupListViewModel { get; }

        public Market2ViewModel(ProjectLookupListViewModel projectListViewModel, OfferLookupListViewModel offerListViewModel, 
            UnitLookupListViewModel unitLookupListViewModel, IEventAggregator eventAggregator, 
            OfferLookupDataService offerLookupDataService)
        {
            ProjectListViewModel = projectListViewModel;
            OfferListViewModel = offerListViewModel;
            UnitLookupListViewModel = unitLookupListViewModel;

            ProjectListViewModel.SelectedLookupChanged += async project =>
            {
                UnitLookupListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
                foreach (var offerLookup in project.Offers)
                {
                    if (offerLookup.OfferUnits == null)
                    {
                        offerLookup.OfferUnits = (await offerLookupDataService.GetLookupById(offerLookup.Id)).OfferUnits.ToList();
                    }
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
