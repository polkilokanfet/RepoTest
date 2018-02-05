using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.UI.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(SalesCRUD))]
    public partial class MarketView
    {
        public MarketView(IRegionManager regionManager, IEventAggregator eventAggregator, 
                          MarketProjectListViewModel projectListViewModel, 
                          MarketProjectUnitGroupListViewModel projectUnitGroupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            ProjectListView.DataContext = projectListViewModel;
            ProjectUnitGroupListView.DataContext = projectUnitGroupListViewModel;

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            var viewModel = (ProjectListViewModel)ProjectListView.DataContext;
            if (!viewModel.LoadedFlag)
                await viewModel.LoadAsync();
        }
    }
}
