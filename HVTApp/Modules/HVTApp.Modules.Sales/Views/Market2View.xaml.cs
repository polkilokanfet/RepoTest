using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    public partial class Market2View : ViewBase
    {
        private readonly Market2ViewModel _viewModel;

        public Market2View(IRegionManager regionManager, IEventAggregator eventAggregator, Market2ViewModel viewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            _viewModel = viewModel;

            this.DataContext = _viewModel;
            this.ProjectListView.DataContext = _viewModel;
            this.OfferListView.DataContext = _viewModel.OfferListViewModel;
            this.UnitListView.DataContext = _viewModel.UnitLookupListViewModel;

            ProjectListView.ManagerVisibility = Visibility.Collapsed;

            OfferListView.AuthorVisibility = Visibility.Collapsed;
            OfferListView.ProjectVisibility = Visibility.Collapsed;
            OfferListView.RegistrationDetailsOfRecipientVisibility = Visibility.Collapsed;
            OfferListView.RegistrationDetailsOfSenderVisibility = Visibility.Collapsed;
            OfferListView.RecipientEmployeeVisibility = Visibility.Collapsed;
            OfferListView.SenderEmployeeVisibility = Visibility.Collapsed;
            OfferListView.VatVisibility = Visibility.Collapsed;
            OfferListView.RequestDocumentVisibility = Visibility.Collapsed;

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _viewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
