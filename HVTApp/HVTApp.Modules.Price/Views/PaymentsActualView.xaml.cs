using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabPaymentsActual))]
    public partial class PaymentsActualView
    {
        public PaymentsActualView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
