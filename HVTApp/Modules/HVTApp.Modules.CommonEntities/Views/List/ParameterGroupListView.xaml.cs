using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ParameterGroupListView
    {
        public ParameterGroupListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
