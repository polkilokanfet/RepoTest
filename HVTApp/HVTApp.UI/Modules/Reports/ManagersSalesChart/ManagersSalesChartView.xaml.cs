using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.ManagersSalesChart
{
    [RibbonTab(typeof(TabManagersSalesChart))]
    public partial class ManagersSalesChartView
    {
        public ManagersSalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
