using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.PriceEngineering.InvoiceForPayment
{
    [RibbonTab(typeof(TabInvoiceForPaymentTasksView))]
    public partial class InvoiceForPaymentTasksView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;

        public InvoiceForPaymentTasksView(InvoiceForPaymentTasksViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            InitializeComponent();
        }
    }
}
