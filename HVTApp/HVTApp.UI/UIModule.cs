using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.PlanAndEconomy.Supervision;
using HVTApp.UI.Modules.Products.Views;
using HVTApp.UI.Modules.Reports.FlatReport;
using HVTApp.UI.Modules.Reports.FlatReport.Comparator;
using HVTApp.UI.Modules.Reports.FlatReport.Reports;
using HVTApp.UI.Modules.Reports.MarketReport;
using HVTApp.UI.Modules.Reports.Reference;
using HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.MarketCapacityChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ProducersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart;
using HVTApp.UI.Modules.Reports.Views;
using HVTApp.UI.Modules.SupplyModule.Views;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI
{
    public partial class UiModule : ModuleBase
    {
        private readonly IDialogService _dialogService;
        private readonly ISelectService _selectService;
        private readonly IUpdateDetailsService _updateDetailsService;

        public UiModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService,
            ISelectService selectService, IUpdateDetailsService updateDetailsService) : base(container, regionManager)
        {
            _dialogService = dialogService;
            _selectService = selectService;
            _updateDetailsService = updateDetailsService;
        }

        protected override void RegisterTypes()
        {
            Container.Resolve<IDialogService>().Register<PriceCalculationItemsViewModel, PriceCalculationItemsWindow>();
            Container.RegisterViewForNavigation<PriceCalculationView>();
            Container.RegisterViewForNavigation<PriceCalculationsView>();
            Container.RegisterViewForNavigation<ReferenceView>();
            Container.RegisterViewForNavigation<SalesChartView>();
            Container.RegisterViewForNavigation<ProductTypeDesignationView>();
            Container.RegisterViewForNavigation<PaymentsActualView>();
            Container.RegisterViewForNavigation<PickingDatesView>();
            Container.RegisterViewForNavigation<SupplyPlanView>();
            Container.RegisterViewForNavigation<PaymentsPlanView>();
            Container.RegisterViewForNavigation<IncomingRequestsView>();
            Container.RegisterViewForNavigation<MarketReportView>();
            Container.RegisterViewForNavigation<FlatReportView>();
            Container.RegisterViewForNavigation<ManagersSalesChartView>();
            Container.RegisterViewForNavigation<ProductTypesSalesChartView>();
            Container.RegisterViewForNavigation<RegionsSalesChartView>();
            Container.RegisterViewForNavigation<ConsumersSalesChartView>();
            Container.RegisterViewForNavigation<ContragentsSalesChartView>();
            Container.RegisterViewForNavigation<ProducersSalesChartView>();
            Container.RegisterViewForNavigation<MarketCapacityChartView>();
            Container.RegisterViewForNavigation<DirectumTaskView>();
            Container.RegisterViewForNavigation<DirectumTasksOutgoingView>();
            Container.RegisterViewForNavigation<DirectumTasksIncomingView>();
            Container.RegisterViewForNavigation<ProductReplacementView>();
            Container.RegisterViewForNavigation<SupervisionView>();
            Container.RegisterViewForNavigation<HVTApp.UI.Modules.Sales.Supervision.SupervisionView>();
            Container.RegisterViewForNavigation<BudgetComparisionView>();

            Container.Resolve<IDialogService>().Register<DirectumTaskRouteViewModel, DirectumTaskRouteWindow>();

            _dialogService.RegisterShow<ProductStructureViewModel, ProductStructureView>();
            _dialogService.RegisterShow<BudgetComparisionViewModel, BudgetComparisionView>();
            _dialogService.RegisterShow<SalesReportViewModel, SalesReportView>();
            _dialogService.RegisterShow<PaymentsPlanViewModel, PaymentsPlanView>();

            RegisterViews();
        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}