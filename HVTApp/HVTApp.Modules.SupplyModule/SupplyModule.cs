using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.SupplyModule.Menus;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.SupplyModule
{
    [ModuleAccess(Role.Admin, Role.Supplier)]
    public class SupplyModule : ModuleBase
    {
        public SupplyModule(IRegionManager regionManager, IUnityContainer container) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SupplyMenu>());
        }
    }
}