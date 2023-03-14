using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.View
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListViewDesignDepartmentHead
    {

        public PriceEngineeringTasksListViewDesignDepartmentHead(PriceEngineeringTasksListViewModelDesignDepartmentHead priceEngineeringTasksListViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = priceEngineeringTasksListViewModel;
        }
    }
}
