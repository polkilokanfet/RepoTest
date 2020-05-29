using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.PriorityReport
{
    [RibbonTab(typeof(TabReload))]
    public partial class PriorityReportView
    {
        public PriorityReportView(PriorityReportViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
