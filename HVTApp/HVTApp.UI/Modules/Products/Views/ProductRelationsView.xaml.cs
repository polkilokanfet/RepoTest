using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Products.Tabs;
using HVTApp.UI.Modules.Products.ViewModels;
using HVTApp.UI.Tabs;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.Views
{
    [RibbonTab(typeof(TabCRUD)), RibbonTab(typeof(TabRefresh)), RibbonTab(typeof(TabProductRelations))]
    public partial class ProductRelationsView
    {
        public ProductRelationsView(ProductRelationsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            viewModel.Load();
        }
    }
}
