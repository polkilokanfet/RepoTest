using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListViewDesignDepartmentHead : ViewBase
    {
        public PriceEngineeringTasksListViewModel PriceEngineeringTasksListViewModel { get; }
        public WorkloadOnEmployeesViewModel WorkloadOnEmployeesViewModel { get; }

        public PriceEngineeringTasksListViewDesignDepartmentHead(PriceEngineeringTasksListViewModel priceEngineeringTasksListViewModel, WorkloadOnEmployeesViewModel workloadOnEmployeesViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            PriceEngineeringTasksListViewModel = priceEngineeringTasksListViewModel;
            WorkloadOnEmployeesViewModel = workloadOnEmployeesViewModel;
            InitializeComponent();
            DataContext = priceEngineeringTasksListViewModel;
        }
    }
}
