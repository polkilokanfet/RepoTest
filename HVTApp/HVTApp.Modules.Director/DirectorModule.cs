using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Director.Menus;
using HVTApp.Modules.Director.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.Director
{
    [ModuleAccess(Role.Admin, Role.Director)]
    public class DirectorModule : ModuleBase
    {
        public DirectorModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<MarketView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<DirectorMenu>());
        }
    }
}