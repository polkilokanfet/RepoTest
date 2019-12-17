using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Sales.Tabs;
using HVTApp.UI.Modules.Sales.ViewModels;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Views
{
    [RibbonTab(typeof(ProductionTab))]
    public partial class ProductionView
    {
        public ProductionView(ProductionViewModel productionViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = productionViewModel;
        }
    }
}
