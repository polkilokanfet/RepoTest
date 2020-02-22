using HVTApp.Infrastructure;
using HVTApp.UI.Modules.SupplyModule.Tabs;
using HVTApp.UI.Modules.SupplyModule.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.SupplyModule.Views
{
    [RibbonTab(typeof(TabDates))]
    public partial class PickingDatesView
    {
        public PickingDatesView(PickingDatesViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
