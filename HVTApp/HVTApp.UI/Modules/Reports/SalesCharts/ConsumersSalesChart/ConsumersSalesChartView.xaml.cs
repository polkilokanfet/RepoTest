using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart
{
    [RibbonTab(typeof(TabSalesChart))]
    public partial class ConsumersSalesChartView
    {
        public ConsumersSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
