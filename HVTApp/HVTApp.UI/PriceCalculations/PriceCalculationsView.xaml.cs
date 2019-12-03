using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations
{
    [RibbonTab(typeof(TabPriceCalculations))]
    public partial class PriceCalculationsView : ViewBase
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
