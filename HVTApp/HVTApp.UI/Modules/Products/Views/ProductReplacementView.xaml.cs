using HVTApp.UI.Modules.Products.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Products.Views
{
    public partial class ProductReplacementView
    {
        public ProductReplacementView(ProductReplacementViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
