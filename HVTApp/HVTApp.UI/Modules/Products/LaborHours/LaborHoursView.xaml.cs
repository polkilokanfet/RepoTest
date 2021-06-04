using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Products.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.LaborHours
{
    [RibbonTab(typeof(LaborHoursViewTab))]
    public partial class LaborHoursView
    {
        public LaborHoursView(LaborHoursViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
