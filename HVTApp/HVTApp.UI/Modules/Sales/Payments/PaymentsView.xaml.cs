using System.IO;
using HVTApp.Infrastructure;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Payments
{
    [RibbonTab(typeof(PaymentsTab))]
    public partial class PaymentsView : ViewBaseSaveCustomization
    {
        protected override string FileName => "paymentsPlannedSalesCustomisation.xml";

        protected override XamDataGrid DataGrid => (XamDataGrid)this.LoadbleControl.Content;

        public PaymentsView(PaymentsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(viewModel, regionManager, eventAggregator)
        {
            InitializeComponent();
        }

    }
}
