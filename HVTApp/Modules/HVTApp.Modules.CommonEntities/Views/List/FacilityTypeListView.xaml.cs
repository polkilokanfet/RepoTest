using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class FacilityTypeListView
    {
        public FacilityTypeListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
