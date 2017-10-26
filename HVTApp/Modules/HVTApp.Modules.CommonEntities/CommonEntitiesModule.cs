using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.CommonEntities.Menus;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using ActivityFildsView = HVTApp.UI.Views.ActivityFildsView;
using CompaniesView = HVTApp.UI.Views.CompaniesView;
using CompanyDetailsWindow = HVTApp.UI.Views.CompanyDetailsWindow;
using CompanyFormDetailsView = HVTApp.UI.Views.CompanyFormDetailsView;
using CompanyFormsView = HVTApp.UI.Views.CompanyFormsView;
using FacilitiesView = HVTApp.UI.Views.FacilitiesView;
using FacilityTypesView = HVTApp.UI.Views.FacilityTypesView;
using ParametersGroupsView = HVTApp.UI.Views.ParametersGroupsView;
using ParametersView = HVTApp.UI.Views.ParametersView;
using ProductsView = HVTApp.UI.Views.ProductsView;

namespace HVTApp.UI
{
    public class CommonEntitiesModule : ModuleBase
    {
        private readonly IDialogService _dialogService;

        public CommonEntitiesModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService) : base(container, regionManager)
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

            _dialogService.Register<CompanyFormDetailsViewModel, CompanyFormDetailsView>();
            _dialogService.Register<CompanyDetailsViewModel, CompanyDetailsWindow>();
            _dialogService.Register<ProductDetailsViewModel, ProductDetailsView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<CommonEntitiesMenu>());
        }
    }
}