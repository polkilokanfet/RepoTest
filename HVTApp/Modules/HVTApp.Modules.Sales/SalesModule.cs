using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Menus;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales
{
    [RoleToUpdate(Role.Admin, Role.SalesManager)]
    public class SalesModule : ModuleBase
    {
        private readonly IDialogService _dialogService;
        public SalesModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService) : base(container, regionManager)
        {
            _dialogService = dialogService;
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<MarketView>();
            Container.RegisterViewForNavigation<OitView>();
            Container.RegisterViewForNavigation<Market2View>();
            Container.RegisterViewForNavigation<PaymentsPlannedView>();
            Container.RegisterViewForNavigation<OffersView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}