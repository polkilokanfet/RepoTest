using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    [RibbonTab(typeof(TabTechnicalRequrementsTasksView))]
    public partial class TechnicalRequrementsTasksView : ViewBase
    {
        public TechnicalRequrementsTasksView(TechnicalRequrementsTasksViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
