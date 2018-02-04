using System;
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
                          MarketTenderListViewModel tenderListViewModel, 
                          MarketTenderUnitGroupListViewModel tenderUnitGroupListViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            ProjectListView.DataContext = projectListViewModel;
            TenderListView.DataContext = tenderListViewModel;
            TenderUnitGroupListView.DataContext = tenderUnitGroupListViewModel;

            Loaded += OnLoaded;
        }

        private bool _loaded = false;
        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (_loaded) return;
            await ((ProjectListViewModel)ProjectListView.DataContext).LoadAsync();
            _loaded = true;
        }
    }
}
