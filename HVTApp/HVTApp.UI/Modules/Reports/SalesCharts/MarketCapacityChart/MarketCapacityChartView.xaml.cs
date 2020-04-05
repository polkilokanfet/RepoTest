using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesCharts.MarketCapacityChart
{
    [RibbonTab(typeof(TabSalesChart))]
    public partial class MarketCapacityChartView
    {
        public MarketCapacityChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
