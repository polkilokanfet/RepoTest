using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Market.Tabs;
using HVTApp.UI.Modules.Sales.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views.MarketView
{
    [RibbonTab(typeof(MarketMainTab))]
    public partial class MarketView
    {
        public MarketView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
