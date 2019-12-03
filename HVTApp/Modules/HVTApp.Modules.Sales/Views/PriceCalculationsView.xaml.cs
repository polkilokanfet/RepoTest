using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Tabs;
using HVTApp.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    [RibbonTab(typeof(TabPriceCalculations))]
    public partial class PriceCalculationsView
    {
        private PriceCalculationsViewModel _viewModel;
        public PriceCalculationsView(PriceCalculationsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            _viewModel = viewModel;
            this.DataContext = viewModel;
        }
    }
}
