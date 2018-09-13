using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Price.Menus;
using HVTApp.Modules.Price.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Price
{
    public class PriceModule : ModuleBase
    {
        public PriceModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<PricesView>();
            Container.RegisterViewForNavigation<ProductionPlanView>();
            Container.RegisterViewForNavigation<DatesView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}