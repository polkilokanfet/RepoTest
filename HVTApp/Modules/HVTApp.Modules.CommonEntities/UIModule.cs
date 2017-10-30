using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.UI
{
    public class UiModule : ModuleBase
    {
        private readonly IDialogService _dialogService;

        public UiModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService) : base(container, regionManager)
        {
            _dialogService = dialogService;
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<CompanyListView>();
            Container.RegisterViewForNavigation<CompanyFormListView>();
            Container.RegisterViewForNavigation<ActivityFildListView>();
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

            _dialogService.Register<CompanyFormDetailsViewModel, CompanyFormDetailsView>();
            _dialogService.Register<CompanyDetailsViewModel, CompanyDetailsWindow>();
            _dialogService.Register<ProductDetailsViewModel, ProductDetailsView>();
            _dialogService.Register<ProjectDetailsViewModel, ProjectDetailsWindow>();
            _dialogService.Register<OfferDetailsViewModel, OfferDetailsWindow>();
            _dialogService.Register<ProjectUnitsDetailsViewModel, ProductUnitsDetailsWindow>();
        }

        protected override void ResolveOutlookGroup()
        {
        }
    }
}