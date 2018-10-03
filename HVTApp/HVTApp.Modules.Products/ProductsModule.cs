using System.ComponentModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Products.Menus;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.Products
{
    public class ProductsModule : ModuleBase
    {
        public ProductsModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            //Container.RegisterViewForNavigation<PasswordView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<ProductsMenu>());
        }
    }
}