using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Views
{
    public partial class ProductListView 
    {
        public ProductListView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
