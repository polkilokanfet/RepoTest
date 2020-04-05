using HVTApp.Infrastructure;
using Infragistics.Controls.Charts;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart
{
    [RibbonTab(typeof(TabSalesChart))]
    public partial class ManagersSalesChartView
    {
        public ManagersSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }

        private void pieChart_SliceClick(object sender, SliceClickEventArgs e)
        {
            e.IsExploded = !e.IsExploded;
        }

    }
}
