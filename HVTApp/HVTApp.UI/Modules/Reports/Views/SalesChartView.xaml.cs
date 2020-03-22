using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.Views
{
    //[RibbonTab(typeof(TabReload))]
    public partial class SalesChartView
    {
        public SalesChartView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
