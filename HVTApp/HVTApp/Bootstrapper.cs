using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using Prism.Unity;
using HVTApp.Views;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.AuthenticationService;
using HVTApp.Infrastructure.Interfaces.Services.ChooseService;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.BaseEntities;
using HVTApp.Modules.Production;
using HVTApp.Modules.Sales;
using HVTApp.Services.GetProductService;
using HVTApp.Services.ChooseService;
using HVTApp.Services.WpfAuthenticationService;
using HVTApp.Services.DialogService;
using HVTApp.Services.GenerateCalculatePriceTasksService;
using HVTApp.Services.MessageService;
using HVTApp.Services.SelectService;
using HVTApp.Services.UpdateDetailsService;
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

        protected override async void InitializeShell()
        {
            var commonOptions = await Container.Resolve<IUnitOfWork>().GetRepository<CommonOption>().GetAllAsync();
            var commonOption = commonOptions.First();
            CommonOptions.OurCompanyId = commonOption.OurCompanyId;
            CommonOptions.CalculationPriceTerm = commonOption.CalculationPriceTerm;
            CommonOptions.StandartPaymentsConditionSetId = commonOption.StandartPaymentsConditionSetId;
            CommonOptions.StandartTermFromStartToEndProduction = commonOption.StandartTermFromStartToEndProduction;
            CommonOptions.StandartTermFromPickToEndProduction = commonOption.StandartTermFromPickToEndProduction;

            //AuthenticationService authenticationService = (AuthenticationService)_container.Resolve<IAuthenticationService>();
            //if (!authenticationService.AuthenticationAsync())
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
            Container.RegisterType<ISelectService, SelectServiceWpf>(new ContainerControlledLifetimeManager());
            Container.RegisterType<IMessageService, MessageServiceWpf>();
            Container.RegisterType<IGenerateCalculatePriceTasksService, GenerateCalculatePriceTasksServiceRealization>(new ContainerControlledLifetimeManager());

            Container.RegisterType<IProjectLookupDataService, ProjectLookupDataService>();
            Container.RegisterType<ICompanyLookupDataService, CompanyLookupDataService>();
            Container.RegisterType<ICompanyFormLookupDataService, CompanyFormLookupDataService>();

            Container.RegisterType<WrapperDataService>();

            Container.RegisterType<IUpdateDetailsService, UpdateDetailsServiceWpf>(new ContainerControlledLifetimeManager());

            Container.RegisterInstance(typeof(IDialogService), new DialogService((Window)Shell));
            Container.RegisterInstance(typeof(IChooseService), new ChooseService((Window)Shell));
            Container.RegisterType<IGetProductService, GetProductServiceWpf>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();

            catalog.AddModule(typeof(UiModule));
            catalog.AddModule(typeof(SalesModule));
            catalog.AddModule(typeof(BaseEntitiesModule));
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
