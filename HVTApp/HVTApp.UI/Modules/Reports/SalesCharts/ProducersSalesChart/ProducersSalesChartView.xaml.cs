using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ProducersSalesChart
{
    [RibbonTab(typeof(TabSalesChart))]
    public partial class ProducersSalesChartView
    {
        public ProducersSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
