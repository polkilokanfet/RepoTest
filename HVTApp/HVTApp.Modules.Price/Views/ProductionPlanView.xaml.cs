using HVTApp.Infrastructure;
using HVTApp.Modules.PlanAndEconomy.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.PlanAndEconomy.Views
{
    [RibbonTab(typeof(TabProductionPlan))]
    public partial class ProductionPlanView
    {
        public ProductionPlanView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
