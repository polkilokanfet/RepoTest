using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum
{
    [RibbonTab(typeof(TabDirectumTasks))]
    public partial class DirectumTasksIncomingView
    {
        public DirectumTasksIncomingView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
