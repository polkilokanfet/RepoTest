using HVTApp.Infrastructure;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Production
{
    [RibbonTab(typeof(ProductionTab))]
    public partial class ProductionView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;


        public ProductionView(ProductionViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(viewModel, regionManager, eventAggregator)
        {
            InitializeComponent();
        }
    }
}
