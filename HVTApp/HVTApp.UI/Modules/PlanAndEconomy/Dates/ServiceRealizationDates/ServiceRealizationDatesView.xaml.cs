using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.Dates.ServiceRealizationDates
{
    [RibbonTab(typeof(TabServiceRealizationDates))]
    public partial class ServiceRealizationDatesView
    {
        public ServiceRealizationDatesView(ServiceRealizationDatesViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
