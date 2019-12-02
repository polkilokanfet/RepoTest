using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Sales.Views
{
    public partial class SalesUnitsPriceCalculationGroupsView
    {
        public SalesUnitsPriceCalculationGroupsView(IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
