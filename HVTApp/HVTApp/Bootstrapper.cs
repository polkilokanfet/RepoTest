using System;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
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
using HVTApp.Services.MessageService;
using HVTApp.Services.OfferToDocService;
using HVTApp.Services.SelectService;
using HVTApp.Services.UpdateDetailsService;
using HVTApp.UI;
using HVTApp.UI.Lookup;
using HVTApp.UI.Services;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Infragistics.Windows.OutlookBar;
using Infragistics.Windows.Ribbon;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace HVTApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            var mainWindow = Container.Resolve<MainWindow>();
            Application.Current.MainWindow = mainWindow;
            //Завершить приложение при закрытии главного окна.
            Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            return mainWindow;
        }

        protected override async void InitializeShell()
        {
            await SetCommonOptions();

            //await Container.Resolve<IAuthenticationService>().AuthenticationAsync();

            Application.Current.MainWindow.Show();
        }

        /// <summary>
        /// Установка общих опций для всех (наша компания, стандартный срок изготовления и т.д.)
        /// </summary>
        /// <returns></returns>
        private async Task SetCommonOptions()
        {
            var commonOptions = await Container.Resolve<IUnitOfWork>().GetRepository<CommonOption>().GetAllAsync();
            var commonOption = commonOptions.First();

            CommonOptions.OurCompanyId = commonOption.OurCompanyId;
            CommonOptions.CalculationPriceTerm = commonOption.CalculationPriceTerm;
            CommonOptions.StandartPaymentsConditionSetId = commonOption.StandartPaymentsConditionSetId;
            CommonOptions.ProductionTerm = commonOption.StandartTermFromStartToEndProduction;
            CommonOptions.AssembleTerm = commonOption.StandartTermFromPickToEndProduction;
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

            Container.RegisterType<IWrapperDataService, WrapperDataService>();

            Container.RegisterType<IUpdateDetailsService, UpdateDetailsServiceWpf>(new ContainerControlledLifetimeManager());

            Container.RegisterInstance(typeof(IDialogService), new DialogService((Window)Shell));
            Container.RegisterInstance(typeof(IChooseService), new ChooseService((Window)Shell));
            Container.RegisterType<IGetProductService, GetProductServiceWpf>();
            Container.RegisterType<IOfferToDoc, OfferToDoc>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            var catalog = new ModuleCatalog();

            catalog.AddModule(typeof(UiModule));

            AddModuleIfInRole(catalog, typeof(SalesModule));
            AddModuleIfInRole(catalog, typeof(BaseEntitiesModule));
            AddModuleIfInRole(catalog, typeof(ProductionModule));

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


        /// <summary>
        /// Загрузка модулей на основе ролей
        /// </summary>
        /// <param name="catalog"></param>
        /// <param name="moduleType"></param>
        private void AddModuleIfInRole(ModuleCatalog catalog, Type moduleType)
        {
            catalog.AddModule(moduleType);

            //var attr = (RoleToUpdateAttribute)(moduleType.GetCustomAttribute(typeof(RoleToUpdateAttribute), true));
            //if (attr != null && attr.Roles.Contains(CommonOptions.User.RoleCurrent))
            //    catalog.AddModule(moduleType);
        }
    }
}
