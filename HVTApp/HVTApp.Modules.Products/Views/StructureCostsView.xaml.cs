using HVTApp.Infrastructure;
using HVTApp.Modules.Products.Tabs;
using HVTApp.Modules.Products.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.Modules.Products.Views
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
