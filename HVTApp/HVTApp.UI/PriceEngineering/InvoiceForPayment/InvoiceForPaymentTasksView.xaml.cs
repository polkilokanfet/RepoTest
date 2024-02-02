using HVTApp.Infrastructure;
using Infragistics.Windows.DataPresenter;

namespace HVTApp.UI.PriceEngineering.InvoiceForPayment
{
    [RibbonTab(typeof(TabInvoiceForPaymentTasksView))]
    public partial class InvoiceForPaymentTasksView
    {
        protected XamDataGrid DataGrid => this.LoadableControl.Content as XamDataGrid;

        public InvoiceForPaymentTasksView()
        {
            InitializeComponent();
        }
    }
}
