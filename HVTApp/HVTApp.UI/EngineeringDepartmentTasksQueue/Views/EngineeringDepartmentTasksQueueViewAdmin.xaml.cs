using HVTApp.Infrastructure;
using HVTApp.UI.EngineeringDepartmentTasksQueue.ViewModels;
using HVTApp.UI.EngineeringDepartmentTasksQueue.Views.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.EngineeringDepartmentTasksQueue.Views
{
    [RibbonTab(typeof(TabEngineeringDepartmentTasksQueueAdmin))]
    public partial class EngineeringDepartmentTasksQueueViewAdmin
    {

        public EngineeringDepartmentTasksQueueViewAdmin(EngineeringDepartmentTasksQueueViewModelAdmin viewModel, 
            IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
