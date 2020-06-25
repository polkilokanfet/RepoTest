using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Shippings
{
    [RibbonTab(typeof(ShippingTab))]
    public partial class ShippingView
    {
        public ShippingView(ShippingViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
