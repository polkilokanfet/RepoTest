using HVTApp.UI.Modules.SupplyModule.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.SupplyModule.Views
{
    public partial class SupplyPlanView
    {
        public SupplyPlanView(SupplyPlanViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
