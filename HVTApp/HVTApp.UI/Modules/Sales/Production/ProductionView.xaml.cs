using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Production
{
    [RibbonTab(typeof(ProductionTab))]
    public partial class ProductionView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;


        public ProductionView(ProductionViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            InitializeComponent();
        }
    }
}
