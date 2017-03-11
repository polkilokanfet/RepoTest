using Prism.Modularity;
using Prism.Regions;
using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.CommonEntities.Menus;
using HVTApp.Modules.CommonEntities.ViewModels;
using HVTApp.Modules.CommonEntities.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.CommonEntities
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

            _dialogService.Register<CompanyFormDetailsViewModel, CompanyFormDetailsView>();
            _dialogService.Register<CompanyDetailsWindowModel, CompanyDetailsWindow>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<CommonEntitiesMenu>());
        }
    }
}