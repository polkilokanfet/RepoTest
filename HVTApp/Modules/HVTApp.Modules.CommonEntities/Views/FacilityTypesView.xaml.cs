using HVTApp.Infrastructure;
using HVTApp.Modules.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.CommonEntities.Views
{
    [RibbonTab(typeof(TabCRUD))]
    public partial class FacilityTypesView
    {
        public FacilityTypesView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
