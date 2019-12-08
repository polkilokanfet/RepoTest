using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Products.Tabs;
using HVTApp.UI.Modules.Products.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.Views
{
    [RibbonTab(typeof(TabStructureCosts))]
    public partial class StructureCostsView
    {
        public StructureCostsView(StructureCostsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
