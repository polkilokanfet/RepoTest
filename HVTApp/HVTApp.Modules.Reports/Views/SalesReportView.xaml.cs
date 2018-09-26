using HVTApp.Infrastructure;
using HVTApp.Modules.Reports.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Reports.Views
{
    [RibbonTab(typeof(TabReload))]
    public partial class SalesReportView
    {
        public SalesReportView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
