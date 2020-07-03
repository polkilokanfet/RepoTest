using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Sales.Menus;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Modules.Sales.Payments;
using HVTApp.UI.Modules.Sales.Shippings;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.Modules.Sales.Views.MarketView;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Sales
{
    [ModuleAccess(Role.Admin, Role.SalesManager)]
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
            Container.RegisterViewForNavigation<OfferView>();
            Container.RegisterViewForNavigation<ProjectView>();
            Container.RegisterViewForNavigation<SpecificationView>();
            Container.RegisterViewForNavigation<SpecificationsView>();

            Container.Resolve<IDialogService>().Register<OfferUnitsViewModel, OfferUnitsWindow>();
            Container.Resolve<IDialogService>().Register<SalesUnitsViewModel, SalesUnitsWindow>();
            Container.Resolve<IDialogService>().Register<TenderViewModel, TenderWindow>();
            Container.Resolve<IDialogService>().Register<ProductsIncludedViewModel, ProductsIncludedWindow>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}