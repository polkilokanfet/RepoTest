using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Views;
using System.Windows;
using EventServiceClient2;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.EventService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Modules.BaseEntities;
using HVTApp.Modules.BookRegistration;
using HVTApp.Modules.Director;
using HVTApp.Modules.DirectumLite;
using HVTApp.Modules.Messenger;
using HVTApp.Modules.PlanAndEconomy;
using HVTApp.Modules.PriceMaking;
using HVTApp.Modules.Products;
using HVTApp.Modules.Reports;
using HVTApp.Modules.Settings;
using HVTApp.Modules.Sales;
using HVTApp.Modules.SupplyModule;
using HVTApp.RegionAdapters;
using HVTApp.Services.GetProductService;
using HVTApp.Services.WpfAuthenticationService;
using HVTApp.Services.DialogService;
using HVTApp.Services.EmailService;
using HVTApp.Services.MessageService;
using HVTApp.Services.NewProductService;
using HVTApp.Services.PriceService;
using HVTApp.Services.PrintService;
using HVTApp.Services.ProductDesignationService;
using HVTApp.Services.SelectService;
using HVTApp.Services.ShippingService;
using HVTApp.Services.UpdateDetailsService;
using HVTApp.UI;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace HVTApp
{
    internal class Bootstrapper : UnityBootstrapper
    {
        public event Action AllModulesAreInitialized;
        public event Action<double> ModuleIsInitialized;

        protected override DependencyObject CreateShell()
        {
            var mainWindow = Container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            //Завершить приложение при закрытии главного окна.
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            return mainWindow;
        }

        private readonly List<Type> _initializedModules = new List<Type>();

        protected override void InitializeShell()
        {
            SetGlobalAppProperties();
            CheckLastDeveloperVizit();
            //Container.Resolve<IEventServiceClient>().Start();


            Container.Resolve<IEventAggregator>().GetEvent<ModuleIsInitializedEvent>().Subscribe(moduleType =>
            {
                _initializedModules.Add(moduleType);

                ModuleIsInitialized?.Invoke((double)_initializedModules.Count / _modules.Count);

                if (_modules.Select(moduleInfo => moduleInfo.ModuleName).AllContainsIn(_initializedModules.Select(type => type.Name)))
                {
                    AllModulesAreInitialized?.Invoke();
                    Application.Current.MainWindow.Show();
                }
            });
        }

        /// <summary>
        /// Установка общих опций для всех (наша компания, стандартный срок изготовления и т.д.)
        /// </summary>
        /// <returns></returns>
        private void SetGlobalAppProperties()
        {
            //репозиторий с опциями
            var repository = Container.Resolve<IUnitOfWork>().Repository<GlobalProperties>();
            //назначение актуальных опций (последние по дате)
            GlobalAppProperties.Actual = repository.GetAll().OrderBy(x => x.Date).Last();
            
            GlobalAppProperties.ProductDesignationService = Container.Resolve<IProductDesignationService>();
            GlobalAppProperties.ShippingService = Container.Resolve<IShippingService>();
            GlobalAppProperties.PriceService = Container.Resolve<IPriceService>();
        }

        private void CheckLastDeveloperVizit()
        {
            if (GlobalAppProperties.Actual.Developer != null)
            {
                var unitOfWork = Container.Resolve<IUnitOfWork>();
                var globalProperties = unitOfWork.Repository<GlobalProperties>().GetAll().OrderBy(x => x.Date).LastOrDefault();
                if (globalProperties != null && GlobalAppProperties.Actual.Developer.Id == GlobalAppProperties.User.Id)
                {
                    globalProperties.LastDeveloperVizit = DateTime.Today;
                    unitOfWork.SaveChanges();
                    GlobalAppProperties.Actual.LastDeveloperVizit = DateTime.Today;
                }

                if (GlobalAppProperties.Actual.LastDeveloperVizit.HasValue && (DateTime.Today - GlobalAppProperties.Actual.LastDeveloperVizit.Value).Days > 90)
                {
                    Application.Current.Shutdown();
                }
            }
        }

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();
            
            Container.RegisterType<IEventAggregator, EventAggregator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<DbContext, HvtAppContext>();
            Container.RegisterType<IUnitOfWork, UnitOfWork>();
            //Container.RegisterType<IUnitOfWorkDisplay, UnitOfWork>(new ContainerControlledLifetimeManager()); //используется в отображении для создания синглтона
            Container.RegisterType<IAuthenticationService, AuthenticationService>();
            Container.RegisterType<ISelectService, SelectServiceWpf>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessageService, MessageServiceWpf>();
            Container.RegisterType<IEmailService, EmailService>();

            Container.RegisterType<IUpdateDetailsService, UpdateDetailsServiceWpf>(new ContainerControlledLifetimeManager());

            Container.RegisterInstance(typeof(IDialogService), new DialogService((Window)Shell));
            Container.RegisterType<IGetProductService, GetProductServiceWpf>(new ContainerControlledLifetimeManager());
            Container.RegisterType<INewProductService, NewProductServiceWpf>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPrintOfferService, PrintOfferService>();
            Container.RegisterType<IPrintProductService, PrintProductService>();
            Container.RegisterType<IPrintSupervisionLetterService, PrintSupervisionLetterService>();
            Container.RegisterType<IPrintBlankLetterService, PrintBlankLetterService>();
            Container.RegisterType<IProductDesignationService, ProductDesignator>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IPriceService, PriceService>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IShippingService, ShippService>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IEventServiceClient, EventServiceClient>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessenger, Messenger>(new ContainerControlledLifetimeManager());

            Container.RegisterInstance(typeof(IModelsStore), new ModelsStore(Container));
        }

        private List<ModuleInfo> _modules;

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();

            catalog.AddModule(typeof(UiModule));

            catalog.AddModuleByRole(typeof(SalesModule));
            catalog.AddModuleByRole(typeof(DirectorModule));
            catalog.AddModuleByRole(typeof(PlanAndEconomyModule));
            catalog.AddModuleByRole(typeof(PriceMakingModule));
            catalog.AddModuleByRole(typeof(SupplyModule));
            catalog.AddModuleByRole(typeof(ProductsModule));
            catalog.AddModuleByRole(typeof(DirectumLiteModule));
            catalog.AddModuleByRole(typeof(BookRegistrationModule));
            catalog.AddModuleByRole(typeof(ReportsModule));
            
            //catalog.AddModule(typeof(MessengerModule));
            catalog.AddModule(typeof(BaseEntitiesModule));
            catalog.AddModule(typeof(SettingsModule));

            _modules = catalog.Modules.ToList();

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
