using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListViewDesignDepartmentHead : ViewBase
    {
        public PriceEngineeringTasksListViewDesignDepartmentHead(PriceEngineeringTasksListViewModelDesignDepartmentHead viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
