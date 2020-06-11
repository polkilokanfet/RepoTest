using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.Reference
{
    //[RibbonTab(typeof(TabReload))]
    public partial class ReferenceView
    {
        public ReferenceView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
