using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ProductUnitDetailsView
    {
        public ProductUnitDetailsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
