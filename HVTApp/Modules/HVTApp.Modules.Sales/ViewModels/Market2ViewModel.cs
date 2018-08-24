using System.Linq;
using HVTApp.UI.Events;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using Prism.Events;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel
    {
        public ProjectListViewModel ProjectListViewModel { get; }
        public OfferListViewModel OfferListViewModel { get; }
        public UnitLookupListViewModel UnitLookupListViewModel { get; }

        public Market2ViewModel(ProjectListViewModel projectListViewModel, OfferListViewModel offerListViewModel, 
            UnitLookupListViewModel unitLookupListViewModel, IEventAggregator eventAggregator, 
            OfferUnitLookupDataService offerUnitLookupDataService)
        {
            ProjectListViewModel = projectListViewModel;
            OfferListViewModel = offerListViewModel;
            UnitLookupListViewModel = unitLookupListViewModel;

            ProjectListViewModel.SelectedLookupChanged += project =>
            {
                UnitLookupListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                //if (offer == null) return;
                //UnitLookupListViewModel.Load(offer.OfferUnits);
            };

        }

    }
}
