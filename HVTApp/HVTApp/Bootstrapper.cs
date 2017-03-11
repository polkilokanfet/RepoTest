using System.Data.Entity;
using Microsoft.Practices.Unity;
using Prism.Unity;
using HVTApp.Views;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.CommonEntities;
using HVTApp.Modules.Production;
using HVTApp.Modules.Sales;
using HVTApp.Services.ChooseService;
using HVTApp.Services.WpfAuthenticationService;
using HVTApp.Services.DialogService;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
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

            Container.RegisterType<DbContext, HVTAppContext>();
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            Container.RegisterType<IAuthenticationService, AuthenticationService>();

            DialogService dialogService = new DialogService((Window)Shell);
            Container.RegisterInstance(typeof(IDialogService), dialogService);

            ChooseService chooseService = new ChooseService((Window)Shell);
            Container.RegisterInstance(typeof(IChooseService), chooseService);
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
