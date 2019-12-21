using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Products.Tabs;
using HVTApp.UI.Modules.Products.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.Views
{
    [RibbonTab(typeof(TabProductTypeDesignation))]
    public partial class ProductTypeDesignationView
    {
        public ProductTypeDesignationView(ProductTypeDesignationViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
