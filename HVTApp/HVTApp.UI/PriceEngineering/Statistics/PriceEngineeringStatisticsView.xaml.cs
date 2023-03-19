using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.Statistics
{
    //[RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringStatisticsView : ViewBase
    {
        public PriceEngineeringStatisticsView(PriceEngineeringStatisticsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
