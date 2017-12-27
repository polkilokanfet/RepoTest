using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class OfferDetailsView
    {
        public OfferDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
