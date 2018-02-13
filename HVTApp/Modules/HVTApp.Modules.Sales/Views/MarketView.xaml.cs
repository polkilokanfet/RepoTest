using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(SalesCRUD))]
    public partial class MarketView
    {
        private readonly MarketViewModel _marketViewModel;

        public MarketView(IRegionManager regionManager, IEventAggregator eventAggregator, MarketViewModel marketViewModel) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            _marketViewModel = marketViewModel;
            this.DataContext = _marketViewModel;

            Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs args)
        {
            await _marketViewModel.ProjectListViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
