using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Regions;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class Market2ViewModel : ProjectLookupListViewModel
    {
        private readonly IUnityContainer _container;

        public Offer SelectedOffer => OfferListViewModel?.SelectedItem;

        public OfferLookupListViewModel OfferListViewModel { get; }
        public TenderLookupListViewModel TenderListViewModel { get; }
        public UnitLookupListViewModel UnitListViewModel { get; }
        public NoteLookupListViewModel NoteListViewModel { get; }

        public ICommand NewOfferCommand { get; }
        public ICommand EditOfferCommand { get; }
        public ICommand RemoveOfferCommand { get; }
        public ICommand PrintOfferCommand { get; }

        public ICommand NewNoteCommand { get; }
        public ICommand EditNoteCommand { get; }
        public ICommand RemoveNoteCommand { get; }

        public ICommand NewTenderCommand { get; }
        public ICommand EditTenderCommand { get; }
        public ICommand RemoveTenderCommand { get; }

        private readonly IRegionManager _regionManager;

        public Market2ViewModel(IUnityContainer container) : base(container)
        {
            _container = container;
            _regionManager = Container.Resolve<IRegionManager>();

            //контексты
            OfferListViewModel = container.Resolve<OfferLookupListViewModel>();
            TenderListViewModel = container.Resolve<TenderLookupListViewModel>();
            UnitListViewModel = container.Resolve<UnitLookupListViewModel>();
            NoteListViewModel = container.Resolve<NoteLookupListViewModel>();

            //привязываем команды к соответствующим моделям
            NewOfferCommand = OfferListViewModel.NewItemCommand;
            EditOfferCommand = new DelegateCommand(EditOfferCommand_Execute, () => SelectedOffer != null);
            RemoveOfferCommand = OfferListViewModel.RemoveItemCommand;
            PrintOfferCommand = OfferListViewModel.PrintOfferCommand;

            NewTenderCommand = TenderListViewModel.NewItemCommand;
            EditTenderCommand = TenderListViewModel.EditItemCommand;
            RemoveTenderCommand = TenderListViewModel.RemoveItemCommand;

            NewNoteCommand = NoteListViewModel.NewItemCommand;
            EditNoteCommand = NoteListViewModel.EditItemCommand;
            RemoveNoteCommand = NoteListViewModel.RemoveItemCommand;

            //реакция на смену выбранного проекта
            SelectedLookupChanged += project =>
            {
                UnitListViewModel.Load(project.SalesUnits);
                OfferListViewModel.Load(project.Offers);
                TenderListViewModel.Load(project.Tenders);
                NoteListViewModel.Load(project.Notes);
            };

            OfferListViewModel.SelectedLookupChanged += offer =>
            {
                if (offer == null) return;
                UnitListViewModel.Load(offer.OfferUnits);
                ((DelegateCommand)EditOfferCommand).RaiseCanExecuteChanged();
            };
        }

        private void EditOfferCommand_Execute()
        {
            //var mainRegion = _regionManager.Regions[RegionNames.ContentRegion];
            //mainRegion.NavigationService.Journal.GoBack();

            var prms = new NavigationParameters();
            prms.Add("offer", SelectedOffer);
            _regionManager.RequestNavigate(RegionNames.ContentRegion, typeof(OfferView).FullName, prms);
            
        }
    }
}
