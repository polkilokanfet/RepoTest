using HVTApp.Infrastructure;
using HVTApp.UI.Modules.PlanAndEconomy.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates
{
    [RibbonTab(typeof(TabDates))]
    public partial class DatesView
    {
        public DatesView(DatesViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
