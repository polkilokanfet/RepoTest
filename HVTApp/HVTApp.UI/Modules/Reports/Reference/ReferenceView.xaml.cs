using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.Reference
{
    //[RibbonTab(typeof(TabReload))]
    public partial class ReferenceView
    {
        public ReferenceView(IUnityContainer container, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = container.Resolve<ReferenceViewModel>();
        }
    }
}
