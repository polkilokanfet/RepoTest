using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(SalesCRUD))]
    public partial class Market2View : ViewBase
    {
        private readonly Market2ViewModel _viewModel;

        public Market2View(Market2ViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            //назначаем контексты
            _viewModel = viewModel;
            this.DataContext = _viewModel;

            #region ClearViews

            ProjectListView.ManagerVisibility = Visibility.Collapsed;
            ProjectListView.OffersVisibility = Visibility.Collapsed;
            ProjectListView.TendersVisibility = Visibility.Collapsed;
            ProjectListView.SalesUnitsVisibility = Visibility.Collapsed;
            ProjectListView.NotesVisibility = Visibility.Collapsed;
            ProjectListView.HighProbabilityVisibility = Visibility.Collapsed;

            OfferListView.NumberVisibility = Visibility.Collapsed;
            OfferListView.AuthorVisibility = Visibility.Collapsed;
            OfferListView.ProjectVisibility = Visibility.Collapsed;
            OfferListView.RegistrationDetailsOfRecipientVisibility = Visibility.Collapsed;
            OfferListView.RecipientEmployeeVisibility = Visibility.Collapsed;
            OfferListView.SenderEmployeeVisibility = Visibility.Collapsed;
            OfferListView.VatVisibility = Visibility.Collapsed;
            OfferListView.RequestDocumentVisibility = Visibility.Collapsed;
            OfferListView.OfferUnitsVisibility = Visibility.Collapsed;
            OfferListView.CodeVisibility = Visibility.Collapsed;
            OfferListView.RegNumberVisibility = Visibility.Collapsed;

            #endregion

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _viewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
