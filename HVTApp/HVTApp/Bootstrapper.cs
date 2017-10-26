using System.Data.Entity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using HVTApp.Views;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.DataAccess.Infrastructure;
using HVTApp.DataAccess.Lookup;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Wrapper;
using HVTApp.Modules.Production;
using HVTApp.Modules.Sales;
using HVTApp.Modules.Sales.ViewModels;
using HVTApp.Modules.Sales.Views;
using HVTApp.Services.GetProductService;
using HVTApp.Services.ChooseService;
using HVTApp.Services.WpfAuthenticationService;
using HVTApp.Services.DialogService;
using HVTApp.Services.SelectService;
using HVTApp.UI;
using HVTApp.UI.ViewModels;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using CompaniesView = HVTApp.UI.Views.CompaniesView;
using ProjectsView = HVTApp.UI.Views.ProjectsView;
using TendersView = HVTApp.UI.Views.TendersView;

namespace HVTApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            //AuthenticationService authenticationService = (AuthenticationService)Container.Resolve<IAuthenticationService>();
            //if (!authenticationService.Authentication())
            //    Application.Current.Shutdown();

            Application.Current.MainWindow.Show();
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DbContext, HvtAppContext>();
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            Container.RegisterType<IAuthenticationService, AuthenticationService>();
            Container.RegisterType<ISelectService, SelectServiceRealization>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IProjectLookupDataService, ProjectLookupDataService>();
            Container.RegisterType<ICompanyLookupDataService, CompanyLookupDataService>();

            ISelectService selectService = Container.Resolve<ISelectService>();
            selectService.Register<CompaniesViewModel, CompaniesView, CompanyLookup>();
            selectService.Register<ProjectsViewModel, ProjectsView, ProjectLookup>();
            selectService.Register<TendersViewModel, TendersView, TenderLookup>();

            DialogService dialogService = new DialogService((Window)Shell);
            Container.RegisterInstance(typeof(IDialogService), dialogService);

            ChooseService chooseService = new ChooseService((Window)Shell);
            Container.RegisterInstance(typeof(IChooseService), chooseService);

            Container.RegisterType<IGetProductService, GetProductServiceWpf>();
        }
        
        protected override IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            catalog.AddModule(typeof(SalesModule));
            catalog.AddModule(typeof(ProductionModule));
            catalog.AddModule(typeof(CommonEntitiesModule));

            return catalog;
        }

        protected override RegionAdapterMappings ConfigureRegionAdapterMappings()
        {
            var mappings = base.ConfigureRegionAdapterMappings();
            mappings.RegisterMapping(typeof(XamOutlookBar), Container.Resolve<XamOutlookBarRegionAdapter>());
            mappings.RegisterMapping(typeof(XamRibbon), Container.Resolve<XamRibbonRegionAdapter>());
            return mappings;
        }

        protected override IRegionBehaviorFactory ConfigureDefaultRegionBehaviors()
        {
            IRegionBehaviorFactory behaviors = base.ConfigureDefaultRegionBehaviors();
            behaviors.AddIfMissing(XamRibbonRegionBehavior.BehaviorKey, typeof(XamRibbonRegionBehavior));
            return behaviors;
        }
    }
}
