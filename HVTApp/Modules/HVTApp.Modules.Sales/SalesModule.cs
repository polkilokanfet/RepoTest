using Prism.Modularity;
using Prism.Regions;
using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Sales.Menus;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.Modules.Sales.Views;
using Microsoft.Practices.Unity;

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
            Container.RegisterViewForNavigation<ProjectsView>();
            Container.RegisterViewForNavigation<TendersView>();
            Container.RegisterViewForNavigation<OffersView>();
            Container.RegisterViewForNavigation<PaymentsView>();
            Container.RegisterViewForNavigation<ContractsView>();

            _dialogService.Register<ProjectDetailsViewModel, ProjectDetailsWindow>();
            _dialogService.Register<OfferDetailsWindowModel, OfferDetailsWindow>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}