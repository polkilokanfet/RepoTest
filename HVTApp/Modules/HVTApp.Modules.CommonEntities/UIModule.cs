using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Sales.ViewModels;
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
            Container.RegisterViewForNavigation<CompaniesView>();
            Container.RegisterViewForNavigation<CompanyFormsView>();
            Container.RegisterViewForNavigation<ActivityFildsView>();
            Container.RegisterViewForNavigation<ParametersView>();
            Container.RegisterViewForNavigation<ParametersGroupsView>();
            Container.RegisterViewForNavigation<ProductsView>();
            Container.RegisterViewForNavigation<FacilitiesView>();
            Container.RegisterViewForNavigation<FacilityTypesView>();
            Container.RegisterViewForNavigation<ProjectsView>();
            Container.RegisterViewForNavigation<TendersView>();
            Container.RegisterViewForNavigation<OffersView>();
            Container.RegisterViewForNavigation<PaymentsView>();
            Container.RegisterViewForNavigation<ContractsView>();

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