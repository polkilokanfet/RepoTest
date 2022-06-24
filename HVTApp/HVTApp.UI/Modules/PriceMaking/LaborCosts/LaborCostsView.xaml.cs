using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PriceMaking.LaborCosts
{
    [RibbonTab(typeof(LaborCostsViewTab))]
    public partial class LaborCostsView
    {
        public LaborCostsView(LaborCostsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) 
            : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

    }
}
