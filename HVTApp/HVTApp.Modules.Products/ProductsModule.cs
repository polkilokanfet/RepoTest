using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Products.Menus;
using HVTApp.UI.Modules.Products.Parameters;
using HVTApp.UI.Modules.Products.Views;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.Products
{
    [ModuleAccess(Role.Admin, Role.Constructor)]
    public class ProductsModule : ModuleBase
    {
        public ProductsModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<ParametersView>();
            Container.RegisterViewForNavigation<CreateNewProductTasksView>();
            Container.RegisterViewForNavigation<StructureCostsView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<ProductsMenu>());
        }
    }
}