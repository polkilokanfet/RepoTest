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
            this.ProjectListView.DataContext = _viewModel.ProjectListViewModel;
            this.OfferListView.DataContext = _viewModel.OfferListViewModel;
            this.UnitListView.DataContext = _viewModel.UnitLookupListViewModel;

            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _viewModel.ProjectListViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
