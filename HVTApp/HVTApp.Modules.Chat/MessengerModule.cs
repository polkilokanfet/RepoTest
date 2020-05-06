using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Messenger.Menus;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.Messenger
{
    //[ModuleAccess(Role.Admin, Role.Supplier)]
    public class MessengerModule : ModuleBase
    {
        public MessengerModule(IRegionManager regionManager, IUnityContainer container) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<MessengerMenu>());
        }
    }
}