using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(ProductionTab))]
    public partial class ProductionView
    {
        private readonly ProductionViewModel _productionViewModel;

        public ProductionView(ProductionViewModel productionViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            _productionViewModel = productionViewModel;
            InitializeComponent();
            this.DataContext = _productionViewModel;
            this.Loaded += OnLoaded;
        }

        private async void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            await _productionViewModel.LoadAsync();
            this.Loaded -= OnLoaded;
        }
    }
}
