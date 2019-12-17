using HVTApp.Infrastructure;
using HVTApp.UI.PriceCalculations.ViewModel;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceCalculations.View
{
    [RibbonTab(typeof(Tabs.TabPriceCalculations))]
    public partial class PriceCalculationsView : ViewBase
    {
        public PriceCalculationsView(PriceCalculationsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
