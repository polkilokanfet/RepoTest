using System.Windows.Input;
using HVTApp.Services.OfferToDocService;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        private readonly IUnityContainer _container;

        public OfferLookupListViewModel OfferListViewModel { get; }
        public UnitLookupListViewModel UnitLookupListViewModel { get; }
        public NoteLookupListViewModel NoteLookupListViewModel { get; }

        public ICommand NewOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            _container = container;
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            UnitLookupListViewModel = container.Resolve<UnitLookupListViewModel>();
            NoteLookupListViewModel = container.Resolve<NoteLookupListViewModel>();


            NewOfferCommand = OfferListViewModel.NewItemCommand;
            EditOfferCommand = OfferListViewModel.EditItemCommand;
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;
            PrintOfferCommand = new DelegateCommand(async () => await container.Resolve<IOfferToDoc>().PrintOfferAsync(OfferListViewModel.SelectedItem.Id));

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
