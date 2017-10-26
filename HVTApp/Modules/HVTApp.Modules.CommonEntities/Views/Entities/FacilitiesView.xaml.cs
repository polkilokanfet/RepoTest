using HVTApp.Infrastructure;
using HVTApp.Modules.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilitiesView
    {
        public FacilitiesView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
