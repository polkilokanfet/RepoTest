using HVTApp.Infrastructure;
using HVTApp.Modules.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Production.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ProductionPlanView
    {
        public ProductionPlanView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
