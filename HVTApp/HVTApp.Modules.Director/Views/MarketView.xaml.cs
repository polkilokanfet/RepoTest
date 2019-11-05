using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Director.Views
{
    //[RibbonTab(typeof(TabReload))]
    public partial class MarketView
    {
        public MarketView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
