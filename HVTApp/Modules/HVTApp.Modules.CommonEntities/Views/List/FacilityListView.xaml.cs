using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class FacilityListView
    {
        public FacilityListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
