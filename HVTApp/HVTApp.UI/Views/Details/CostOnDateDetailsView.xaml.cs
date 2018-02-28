using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class CostOnDateDetailsView
    {
        public CostOnDateDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
