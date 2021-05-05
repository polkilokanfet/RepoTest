using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.SpecificationSignDates
{
    [RibbonTab(typeof(TabSpecificationSignDates))]
    public partial class SpecificationSignDatesView
    {
        public SpecificationSignDatesView(SpecificationSignDatesViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
