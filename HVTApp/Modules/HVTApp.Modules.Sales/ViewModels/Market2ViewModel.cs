using System.Windows.Input;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        public OfferLookupListViewModel OfferListViewModel { get; }
        public UnitLookupListViewModel UnitLookupListViewModel { get; }
        public NoteLookupListViewModel NoteLookupListViewModel { get; }

        public ICommand NewOfferCommand { get; private set; }
        public ICommand EditOfferCommand { get; private set; }
        public ICommand RemoveOfferCommand { get; private set; }


        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            UnitLookupListViewModel = container.Resolve<UnitLookupListViewModel>();
            NoteLookupListViewModel = container.Resolve<NoteLookupListViewModel>();


            NewOfferCommand = OfferListViewModel.NewItemCommand;
            EditOfferCommand = OfferListViewModel.EditItemCommand;
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;


            this.SelectedLookupChanged += project =>
            {
                UnitLookupListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
                NoteLookupListViewModel.Load(project.Notes);
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                UnitLookupListViewModel.Load(offer.OfferUnits);
            };
        }
    }
}
