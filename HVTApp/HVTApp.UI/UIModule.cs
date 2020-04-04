using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using HVTApp.UI.Modules.Products.Views;
using HVTApp.UI.Modules.Reports.FlatReport;
using HVTApp.UI.Modules.Reports.SalesCharts.ConsumersSalesChart;
using HVTApp.UI.Modules.Reports.SalesCharts.ContragentsSalesChart;
using HVTApp.UI.Modules.Reports.Views;
using HVTApp.UI.Modules.SupplyModule.Views;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using ManagersSalesChartView = HVTApp.UI.Modules.Reports.SalesCharts.ManagersSalesChart.ManagersSalesChartView;
using ProductTypesSalesChartView = HVTApp.UI.Modules.Reports.SalesCharts.ProductTypesSalesChart.ProductTypesSalesChartView;
using RegionsSalesChartView = HVTApp.UI.Modules.Reports.SalesCharts.RegionsSalesChart.RegionsSalesChartView;

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
            Container.RegisterViewForNavigation<FlatReportView>();
            Container.RegisterViewForNavigation<ManagersSalesChartView>();
            Container.RegisterViewForNavigation<ProductTypesSalesChartView>();
            Container.RegisterViewForNavigation<RegionsSalesChartView>();
            Container.RegisterViewForNavigation<ConsumersSalesChartView>();
            Container.RegisterViewForNavigation<ContragentsSalesChartView>();

            _dialogService.RegisterShow<ProductStructureViewModel, ProductStructureView>();
            RegisterViews();
        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}