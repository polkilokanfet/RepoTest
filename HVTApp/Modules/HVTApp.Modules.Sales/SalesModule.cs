using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Infrastructure.Services;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Modules.Sales.Menus;
using HVTApp.UI.Modules.Sales.Market;
using HVTApp.UI.Modules.Sales.Payments;
using HVTApp.UI.Modules.Sales.Production;
using HVTApp.UI.Modules.Sales.Shippings;
using HVTApp.UI.Modules.Sales.ViewModels;
using HVTApp.UI.Modules.Sales.ViewModels.Groups;
using HVTApp.UI.Modules.Sales.Views;
using HVTApp.UI.Modules.Sales.Views.MarketView;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Modules.Sales
{
    [ModuleAccess(Role.Admin, Role.SalesManager)]
    public class SalesModule : ModuleBase
    {
        public SalesModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
//#if DEBUG
//#else
//            //проверка на объекты без местоположения
//            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
//            {
//                CheckFacilities(container);
//            }
//#endif
        }

        private static List<Facility> _facilities;
        /// <summary>
        /// Поиск объектов с ошибками.
        /// </summary>
        /// <param name="container"></param>
        private static void CheckFacilities(IUnityContainer container)
        {
            Task.Run(
                () =>
                {
                    var unitOfWork = container.Resolve<IUnitOfWork>();
                    _facilities = unitOfWork.Repository<SalesUnit>()
                        .Find(x => x.Project.Manager.Id == GlobalAppProperties.User.Id)
                        .Select(x => x.Facility)
                        .Distinct()
                        .Where(x => x.GetRegion() == null)
                        .ToList();
                }).Await(
                () =>
                {
                    if (_facilities.Any())
                    {
                        var messageService = container.Resolve<IMessageService>();
                        messageService.ShowOkMessageDialog("Укажите местоположения объектов",
                            "В Ваших проектах задействованы объекты без определенного местоположения. Исправьте это недоразумение.");

                        var updateDetailsService = container.Resolve<IUpdateDetailsService>();
                        foreach (var facility in _facilities)
                        {
                            updateDetailsService.UpdateDetails(facility);
                        }
                    }
                });

        }


        protected override void RegisterTypes()
        {
            //для подгрузки данных заранее
            if (GlobalAppProperties.User.RoleCurrent == Role.SalesManager)
            {
                //Container.RegisterInstance(typeof(Market2ViewModel), new Market2ViewModel(Container));
                Container.RegisterInstance(typeof(IProjectUnitsStore), new ProjectUnitsStore(Container.Resolve<IModelsStore>(), Container.Resolve<IEventAggregator>()));
            }

            Container.RegisterViewForNavigation<Market2View>();
            Container.RegisterViewForNavigation<MarketView>();
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
            Container.Resolve<IDialogService>().Register<SupervisionPriceViewModel, SupervisionPriceWindow>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<SalesMenu>());
        }
    }
}