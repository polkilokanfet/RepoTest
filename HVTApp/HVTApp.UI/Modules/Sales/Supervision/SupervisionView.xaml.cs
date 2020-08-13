using HVTApp.Infrastructure;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Supervision
{
    [RibbonTab(typeof(TabSupervision))]
    public partial class SupervisionView
    {
        public SupervisionView(SupervisionViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
