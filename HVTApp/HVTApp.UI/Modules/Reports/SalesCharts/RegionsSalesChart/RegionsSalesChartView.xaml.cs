using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart
{
    [RibbonTab(typeof(TabSalesChart))]
    public partial class RegionsSalesChartView
    {
        public RegionsSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
