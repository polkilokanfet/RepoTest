using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.PlanAndEconomy.Menus;
using HVTApp.UI.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;


namespace HVTApp.Modules.PlanAndEconomy
{
    [ModuleAccess(Role.Admin, Role.Economist, Role.PlanMaker)]
    public class PlanAndEconomyModule : ModuleBase
    {
        public PlanAndEconomyModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<ProductionPlanView>();
            Container.RegisterViewForNavigation<OrderView>();
            Container.RegisterViewForNavigation<DatesView>();
            Container.RegisterViewForNavigation<PaymentsActualView>();
            Container.RegisterViewForNavigation<PaymentDocumentView>();
            Container.RegisterViewForNavigation<PaymentsView>();
            Container.RegisterViewForNavigation<PaymentsPlanView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<PlanAndEconomyMenu>());
        }
    }
}