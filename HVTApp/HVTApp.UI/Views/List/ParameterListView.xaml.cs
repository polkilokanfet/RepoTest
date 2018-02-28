using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ParameterListView 
    {
        public ParameterListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
