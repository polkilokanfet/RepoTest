using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Reports.FairnessCheck
{
    [RibbonTab(typeof(FairnessCheckTab))]
    public partial class FairnessCheckView
    {
        public FairnessCheckView(FairnessCheckViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
