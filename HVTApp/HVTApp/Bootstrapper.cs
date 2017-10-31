using System.Data.Entity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using HVTApp.Views;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.BaseEntities;
using HVTApp.Modules.Production;
using HVTApp.Modules.Sales;
using HVTApp.Services.GetProductService;
using HVTApp.Services.ChooseService;
using HVTApp.Services.WpfAuthenticationService;
using HVTApp.Services.DialogService;
using HVTApp.Services.MessageService;
using HVTApp.Services.SelectService;
using HVTApp.UI;
using HVTApp.UI.Lookup;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Views;
using HVTApp.UI.Wrapper;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;

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
            Container.RegisterType<DbContext, HvtAppContext>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IUnitOfWork, UnitOfWork>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IAuthenticationService, AuthenticationService>();
            Container.RegisterType<ISelectService, SelectServiceRealization>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessageService, MessageServiceWpf>();

            Container.RegisterType<IProjectLookupDataService, ProjectLookupDataService>();
            Container.RegisterType<ICompanyLookupDataService, CompanyLookupDataService>();
            Container.RegisterType<ICompanyFormLookupDataService, CompanyFormLookupDataService>();

            Container.RegisterType<WrapperDataService>();

            ISelectService selectService = Container.Resolve<ISelectService>();
            selectService.Register<CompanyListViewModel, CompanyListView, CompanyWrapper>();
            selectService.Register<ProjectListViewModel, ProjectListView, ProjectWrapper>();
            selectService.Register<TenderListViewModel, TenderListView, TenderWrapper>();

            DialogService dialogService = new DialogService((Window)Shell);
            Container.RegisterInstance(typeof(IDialogService), dialogService);

            ChooseService chooseService = new ChooseService((Window)Shell);
            Container.RegisterInstance(typeof(IChooseService), chooseService);

            Container.RegisterType<IGetProductService, GetProductServiceWpf>();
        }
        
        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();

            catalog.AddModule(typeof(UiModule));
            catalog.AddModule(typeof(BaseEntitiesModule));
            catalog.AddModule(typeof(SalesModule));
            catalog.AddModule(typeof(ProductionModule));

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
