using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ActivityFieldListView
    {
        public ActivityFieldListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
