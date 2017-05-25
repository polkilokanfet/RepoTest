using HVTApp.Infrastructure;
using HVTApp.Modules.CommonEntities.Menus;
using Prism.Events;
using Prism.Regions;
using TabCRUD = HVTApp.Modules.CommonEntities.Tabs.TabCRUD;

namespace HVTApp.Modules.CommonEntities.Views
{

    [RibbonTab(typeof(TabCRUD))]
    public partial class ActivityFildsView
    {
        public ActivityFildsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
