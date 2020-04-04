using Infragistics.Controls.Charts;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.ManagersSalesChart
{
    //[RibbonTab(typeof(TabReload))]
    public partial class ManagersSalesChartView
    {
        public ManagersSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            StackedBarSeries stackedBarSeries = new StackedBarSeries();
            stackedBarSeries.Series.Add(new StackedFragmentSeries());
            var stackedFragmentSeries = new StackedFragmentSeries();
            stackedFragmentSeries.ValueMemberPath = "";
            stackedFragmentSeries.Title = "";
            //StackedBarChart.Series
        }
    }
}
