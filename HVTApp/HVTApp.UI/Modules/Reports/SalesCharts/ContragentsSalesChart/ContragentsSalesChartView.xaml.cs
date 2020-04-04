using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart
{
    [RibbonTab(typeof(TabSalesChart))]
    public partial class ContragentsSalesChartView
    {
        public ContragentsSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
