using HVTApp.Infrastructure;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Production
{
    [RibbonTab(typeof(ProductionTab))]
    public partial class ProductionView
    {
        protected override string FileName => "productionSalesCustomisation.xml";

        protected override XamDataGrid DataGrid => (XamDataGrid)this.LoadbleControl.Content;


        public ProductionView(ProductionViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(viewModel, regionManager, eventAggregator)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
