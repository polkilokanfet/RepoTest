using HVTApp.Infrastructure;
using HVTApp.UI.PriceEngineering.Tabs;
using HVTApp.UI.PriceEngineering.ViewModel;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering
{
    [RibbonTab(typeof(TabPriceEngineeringTasks))]
    public partial class PriceEngineeringTasksListView : ViewBase
    {
        public PriceEngineeringTasksListView(PriceEngineeringTasksListViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
