using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using Prism.Events;
using Prism.Regions;
using MarketTab = HVTApp.UI.Modules.Sales.Market.Tabs.MarketTab;

namespace HVTApp.UI.Modules.Sales.Views.MarketView
{
    [RibbonTab(typeof(MarketTab))]
    public partial class MarketView
    {
        public MarketView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
