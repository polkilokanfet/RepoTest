using HVTApp.Infrastructure;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class ActivityFieldListView
    {
        public ActivityFieldListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
