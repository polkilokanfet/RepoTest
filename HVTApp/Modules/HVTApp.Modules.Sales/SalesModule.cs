using Prism.Modularity;
using Prism.Regions;
using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Sales.Menus;
using HVTApp.Modules.Sales.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales
{
    public class SalesModule : ModuleBase
    {
        public SalesModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager) { }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<ProjectsView>();
            Container.RegisterViewForNavigation<PaymentsView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}