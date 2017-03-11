using HVTApp.Infrastructure;
using HVTApp.Modules.Sales.Menus;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    /// <summary>
    /// Interaction logic for ProjectsView
    /// </summary>
    [RibbonTab(typeof(TabProjects))]
    public partial class ProjectsView
    {
        public ProjectsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
