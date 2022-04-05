using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    //[RibbonTab(typeof(Tabs.TabPriceCalculations))]
    public partial class PriceEngineeringTasksListView : ViewBase
    {
        public PriceEngineeringTasksListView(PriceEngineeringTasksListViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
