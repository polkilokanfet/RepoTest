using Prism.Modularity;
using Prism.Regions;
using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Sales.Menus;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.Modules.Sales.Views;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;
using ContractsView = HVTApp.UI.Views.ContractsView;
using OfferDetailsWindow = HVTApp.UI.Views.OfferDetailsWindow;
using OffersView = HVTApp.UI.Views.OffersView;
using PaymentsView = HVTApp.UI.Views.PaymentsView;
using ProductUnitsDetailsWindow = HVTApp.UI.Views.ProductUnitsDetailsWindow;
using ProjectDetailsWindow = HVTApp.UI.Views.ProjectDetailsWindow;
using ProjectsView = HVTApp.UI.Views.ProjectsView;
using TendersView = HVTApp.UI.Views.TendersView;

namespace HVTApp.Modules.Sales
{
    public class SalesModule : ModuleBase
    {
        private readonly IDialogService _dialogService;
        public SalesModule(IUnityContainer container, IRegionManager regionManager, IDialogService dialogService) : base(container, regionManager)
        {
            _dialogService = dialogService;
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<MarketView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}