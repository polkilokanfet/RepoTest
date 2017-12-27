using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class OfferListView 
    {
        public OfferListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
