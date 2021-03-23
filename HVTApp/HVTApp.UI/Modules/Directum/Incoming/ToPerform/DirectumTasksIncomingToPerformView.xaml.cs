using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum.ToPerform
{
    [RibbonTab(typeof(TabDirectumTasks))]
    public partial class DirectumTasksIncomingToPerformView
    {
        public DirectumTasksIncomingToPerformView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
