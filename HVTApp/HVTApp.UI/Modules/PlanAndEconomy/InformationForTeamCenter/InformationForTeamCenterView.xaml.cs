using HVTApp.Infrastructure;
using HVTApp.UI.Modules.Reports.CommonInfo;
using HVTApp.UI.PriceEngineering.InvoiceForPayment;
using Prism.Events;
using Prism.Regions;

namespace HVTApp.UI.Modules.PlanAndEconomy.InformationForTeamCenter
{
    [RibbonTab(typeof(TabInvoiceForPaymentTasksView))]
    public partial class InformationForTeamCenterView
    {
        public InformationForTeamCenterView(InvoiceForPaymentTasksViewModel invoiceForPaymentTasksViewModel, CommonInfoViewModel commonInfoViewModel, IRegionManager regionManager, IEventAggregator eventAggregator) : base(regionManager, eventAggregator)
        {
            InitializeComponent();

            this.DataContext = invoiceForPaymentTasksViewModel;
            this.InvoiceForPaymentTasksView.DataContext = invoiceForPaymentTasksViewModel;
            this.CommonInfoView.DataContext = commonInfoViewModel;
        }
    }
}
