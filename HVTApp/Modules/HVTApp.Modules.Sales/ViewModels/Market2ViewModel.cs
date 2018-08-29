using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        public OfferLookupListViewModel OfferListViewModel { get; }
        public UnitLookupListViewModel UnitLookupListViewModel { get; }

        public Market2ViewModel(IUnityContainer container, 
                                OfferLookupListViewModel offerListViewModel, 
                                UnitLookupListViewModel unitLookupListViewModel) : base(container)
        {
            OfferListViewModel = offerListViewModel;
            UnitLookupListViewModel = unitLookupListViewModel;

            this.SelectedLookupChanged += project =>
            {
                UnitLookupListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                UnitLookupListViewModel.Load(offer.OfferUnits);
            };
        }
    }
}
