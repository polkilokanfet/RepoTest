using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.BaseEntities.Menus;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.BaseEntities
{
    public class BaseEntitiesModule : ModuleBase
    {
        private readonly IDialogService _dialogService;

        public BaseEntitiesModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService) : base(container, regionManager)
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