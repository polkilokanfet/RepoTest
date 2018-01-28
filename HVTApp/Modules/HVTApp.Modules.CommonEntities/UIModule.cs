using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI
{
    public class UiModule : ModuleBase
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
            Container.RegisterViewForNavigation<CompanyListView>();
            Container.RegisterViewForNavigation<CompanyFormListView>();
            Container.RegisterViewForNavigation<ActivityFieldListView>();
            Container.RegisterViewForNavigation<ParameterListView>();
            Container.RegisterViewForNavigation<ParameterGroupListView>();
            Container.RegisterViewForNavigation<ProductListView>();
            Container.RegisterViewForNavigation<FacilityListView>();
            Container.RegisterViewForNavigation<FacilityTypeListView>();
            Container.RegisterViewForNavigation<ProjectListView>();
            Container.RegisterViewForNavigation<TenderListView>();
            Container.RegisterViewForNavigation<OfferListView>();
            Container.RegisterViewForNavigation<PaymentListView>();
            Container.RegisterViewForNavigation<ContractListView>();
            Container.RegisterViewForNavigation<PartPriceListView>();

            _dialogService.Register<ProductDetailsViewModel, ProductDetailsView>();
            //_dialogService.Register<ProjectUnitsDetailsViewModel, ProductUnitsDetailsView>();

            _selectService.Register<SelectActivityFieldViewModel, ActivityFieldListView, ActivityFieldWrapper>();
            _selectService.Register<CompanyListViewModel, CompanyListView, CompanyWrapper>();
            _selectService.Register<ProjectListViewModel, ProjectListView, ProjectWrapper>();
            _selectService.Register<TenderListViewModel, TenderListView, TenderWrapper>();

            _updateDetailsService.Register<Company, CompanyDetailsView>();
            _updateDetailsService.Register<CompanyForm, CompanyFormDetailsView>();
            _updateDetailsService.Register<Offer, OfferDetailsView>();
            _updateDetailsService.Register<CostOnDate, CostOnDateDetailsView>();
            _updateDetailsService.Register<Project, ProjectDetailsView>();
            _updateDetailsService.Register<ProjectUnit, ProductUnitsDetailsView>();
        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}