using Prism.Modularity;
using Prism.Regions;
using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Production.Menus;
using HVTApp.Modules.Production.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Production
{
    public class ProductionModule : ModuleBase
    {
        public ProductionModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<ProductionPlanView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<ProductionMenu>());
        }
    }
}