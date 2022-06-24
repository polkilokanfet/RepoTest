using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.PriceMaking.Menus;
using HVTApp.UI.Modules.PriceMaking.LaborCosts;
using HVTApp.UI.Modules.PriceMaking.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.PriceMaking
{
    [ModuleAccess(Role.Admin, Role.Pricer)]
    public class PriceMakingModule : ModuleBase
    {
        public PriceMakingModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<PricesView>();
            Container.RegisterViewForNavigation<LaborCostsView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<PriceMakingMenu>());
        }
    }
}