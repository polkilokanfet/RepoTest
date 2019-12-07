using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.PriceMaking.Menus;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using PricesView = HVTApp.UI.Modules.PriceMaking.Views.PricesView;

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
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<PriceMakingMenu>());
        }
    }
}