using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.PlanAndEconomy.Menus;
using HVTApp.Modules.PlanAndEconomy.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;
using DatesView = HVTApp.Modules.PlanAndEconomy.Views.DatesView;
using PaymentDocumentsView = HVTApp.Modules.PlanAndEconomy.Views.PaymentDocumentsView;
using PaymentsView = HVTApp.Modules.PlanAndEconomy.Views.PaymentsView;
using PricesView = HVTApp.Modules.PlanAndEconomy.Views.PricesView;

namespace HVTApp.Modules.PlanAndEconomy
{
    [ModuleAccess(Role.Admin, Role.Economist, Role.PlanMaker, Role.Pricer)]
    public class PlanAndEconomyModule : ModuleBase
    {
        public PlanAndEconomyModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<PricesView>();
            Container.RegisterViewForNavigation<ProductionPlanView>();
            Container.RegisterViewForNavigation<DatesView>();
            Container.RegisterViewForNavigation<PaymentDocumentsView>();
            Container.RegisterViewForNavigation<PaymentsView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<PlanAndEconomyMenu>());
        }
    }
}