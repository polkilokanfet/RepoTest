using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Directum.ToAccept
{
    [RibbonTab(typeof(TabDirectumTasks))]
    public partial class DirectumTasksIncomingToAcceptView
    {
        public DirectumTasksIncomingToAcceptView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
