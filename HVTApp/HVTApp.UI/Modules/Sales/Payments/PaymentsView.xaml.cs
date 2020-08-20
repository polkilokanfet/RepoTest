using System.IO;
using HVTApp.Infrastructure;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Payments
{
    [RibbonTab(typeof(PaymentsTab))]
    public partial class PaymentsView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;

        public PaymentsView(PaymentsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(viewModel, regionManager, eventAggregator)
        {
            InitializeComponent();
        }

    }
}
