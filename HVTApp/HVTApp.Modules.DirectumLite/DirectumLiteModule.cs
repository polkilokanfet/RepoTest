using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.DirectumLite.Menus;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.DirectumLite
{
    [ModuleAccess(Role.Admin, Role.SalesManager, Role.Economist, Role.Director, Role.Pricer, Role.PlanMaker, Role.ReportMaker)]
    public class DirectumLiteModule : ModuleBase
    {
        public DirectumLiteModule(IRegionManager regionManager, IUnityContainer container) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<DirectumLiteMenu>());
        }
    }
}