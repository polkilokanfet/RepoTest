using System.IO;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using Infragistics.Windows.DataPresenter;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.Sales.Payments
{
    [RibbonTab(typeof(PaymentsTab))]
    public partial class PaymentsView
    {
        protected override XamDataGrid DataGrid => this.LoadbleControl.Content as XamDataGrid;

        public PaymentsView(PaymentsViewModel viewModel, IRegionManager regionManager, IEventAggregator eventAggregator, IMessageService messageService) : base(viewModel, regionManager, eventAggregator, messageService)
        {
            InitializeComponent();
        }

    }
}
