using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Sales.Menus;
using HVTApp.Modules.Sales.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales
{
    [RoleToUpdate(Role.Admin, Role.SalesManager)]
    public class SalesModule : ModuleBase
    {
        public SalesModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<MarketView>();
            Container.RegisterViewForNavigation<Market2View>();
            Container.RegisterViewForNavigation<PaymentsView>();
            Container.RegisterViewForNavigation<ProjectsView>();
            Container.RegisterViewForNavigation<OffersView>();
            Container.RegisterViewForNavigation<ProductionView>();
            Container.RegisterViewForNavigation<ShippingView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}