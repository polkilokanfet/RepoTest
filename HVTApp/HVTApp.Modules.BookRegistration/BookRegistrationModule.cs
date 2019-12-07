using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.BookRegistration.Menus;
using HVTApp.UI.Modules.BookRegistration.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.BookRegistration
{
    [ModuleAccess(Role.Admin, Role.SalesManager, Role.Economist)]
    public class BookRegistrationModule : ModuleBase
    {
        public BookRegistrationModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<BookRegistrationView>();
            Container.RegisterViewForNavigation<DocumentView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<BookRegistrationMenu>());
        }
    }
}