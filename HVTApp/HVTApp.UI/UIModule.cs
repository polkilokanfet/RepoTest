using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Model.POCOs;
using HVTApp.UI.EngineeringDepartmentTasksQueue.Views;
using HVTApp.UI.Modules.BookRegistration.Views;
using HVTApp.UI.Modules.Directum;
using HVTApp.UI.Modules.Directum.ToAccept;
using HVTApp.UI.Modules.Directum.ToPerform;
using HVTApp.UI.Modules.PlanAndEconomy.InformationForTeamCenter;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual;
using HVTApp.UI.Modules.PlanAndEconomy.PaymentsPlan;
using HVTApp.UI.Modules.PlanAndEconomy.Supervision;
using HVTApp.UI.Modules.Products.LaborHours;
using HVTApp.UI.Modules.Products.Views;
using HVTApp.UI.Modules.Reports.CommonInfo;
using HVTApp.UI.Modules.Reports.FairnessCheck;
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
using HVTApp.UI.Modules.Sales.Project1.ViewModels;
using HVTApp.UI.Modules.Sales.Project1.Views;
using HVTApp.UI.Modules.SupplyModule.Views;
using HVTApp.UI.PaymentConditionsSet;
using HVTApp.UI.PriceCalculations.View;
using HVTApp.UI.PriceCalculations.ViewModel;
using HVTApp.UI.PriceEngineering.ParametersService1;
using HVTApp.UI.PriceEngineering.Report;
using HVTApp.UI.PriceEngineering.Statistics;
using HVTApp.UI.PriceEngineering.Tce.List.View;
using HVTApp.UI.PriceEngineering.Tce.Second.View;
using HVTApp.UI.PriceEngineering.View;
using HVTApp.UI.Specifications;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManager;
using HVTApp.UI.TaskInvoiceForPayment1.ForBackManagerBoss;
using HVTApp.UI.TaskInvoiceForPayment1.ForManager;
using HVTApp.UI.TaskInvoiceForPayment1.ForPlanMaker;
using HVTApp.UI.TaskInvoiceForPayment1.List;
using HVTApp.UI.TechnicalRequrementsTasksModule;
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
            Container.RegisterViewForNavigation<SpecificationsViewBase>();
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
            Container.RegisterViewForNavigation<DirectumTasksIncomingToAcceptView>();
            Container.RegisterViewForNavigation<DirectumTasksIncomingToPerformView>();
            Container.RegisterViewForNavigation<ProductReplacementView>();
            Container.RegisterViewForNavigation<SupervisionView>();
            Container.RegisterViewForNavigation<Modules.Sales.Supervision.SupervisionView>();
            Container.RegisterViewForNavigation<BudgetComparisionView>();
            Container.RegisterViewForNavigation<CommonInfoView>();
            Container.RegisterViewForNavigation<TechnicalRequrementsTaskView>();
            Container.RegisterViewForNavigation<TechnicalRequrementsTasksView>();
            Container.RegisterViewForNavigation<LaborHoursView>();
            Container.RegisterViewForNavigation<PaymentConditionSetLookupListView1>();
            Container.RegisterViewForNavigation<InformationForTeamCenterManagerView>();

            Container.RegisterViewForNavigation<PriceEngineeringTasksViewConstructor>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksViewPlanMaker>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksViewDesignDepartmentHead>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksViewInspector>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksViewManager>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksViewBackManager>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksViewBackManagerBoss>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksListView>();

            Container.RegisterViewForNavigation<DesignDepartmentView>();
            Container.RegisterViewForNavigation<ProductRelationsView>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksTceView2>();
            Container.RegisterViewForNavigation<TasksTceView>();
            Container.RegisterViewForNavigation<PriceEngineeringTasksListViewDesignDepartmentHead>();
            Container.RegisterViewForNavigation<EngineeringDepartmentTasksQueueViewAdmin>();
            Container.RegisterViewForNavigation<EngineeringDepartmentTasksQueueViewDepartmentHead>();
            Container.RegisterViewForNavigation<EngineeringDepartmentTasksQueueViewConstructor>();

            Container.RegisterViewForNavigation<PriceEngineeringTasksListViewAdmin>();
            

            Container.RegisterViewForNavigation<PriceEngineeringStatisticsView>();

            Container.RegisterViewForNavigation<FairnessCheckView>();

            Container.RegisterViewForNavigation<TaskInvoiceForPaymentListView>();
            Container.RegisterViewForNavigation<TaskInvoiceForPaymentManagerView>();
            Container.RegisterViewForNavigation<TaskInvoiceForPaymentBackManagerBossView>();
            Container.RegisterViewForNavigation<TaskInvoiceForPaymentBackManagerView>();
            Container.RegisterViewForNavigation<TaskInvoiceForPaymentPlanMakerView>();

            _dialogService.Register<DirectumTaskRouteViewModel, DirectumTaskRouteWindow>();
            _dialogService.Register<PaymentConditionViewModel, PaymentConditionView>();
            _dialogService.Register<ParametersServiceViewModel, ParametersServiceView>();
            _dialogService.Register<ProductIncludedViewModel, ProductIncludedView>();
            _dialogService.Register<ProjectUnitAddViewModel, ProjectUnitAddView>();

            _dialogService.Register<ProductStructureViewModel, ProductStructureView>();
            _dialogService.Register<BudgetComparisionViewModel, BudgetComparisionView>();
            _dialogService.Register<SalesReportViewModel, SalesReportView>();
            _dialogService.Register<PaymentsPlanViewModel, PaymentsPlanView>();
            _dialogService.Register<PriceEngineeringTask, BlockReportView>();
            _dialogService.Register<ProjectUnitEditViewModel, ProjectUnitEditView>();

            RegisterViews();

            _updateDetailsService.ReRegister<Facility, FacilityView>();
            _updateDetailsService.ReRegister<LaborHours, LaborHoursDetailsView2>();
            _updateDetailsService.ReRegister<PaymentConditionSet, PaymentConditionsSetView>();

            _selectService.ReRegister<PaymentConditionSetLookupListView1, PaymentConditionSet>();
            _selectService.ReRegister<UserListView, User>();
        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}