using Prism.Regions;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Reports.Menus;
using HVTApp.Modules.Reports.Views;
using Microsoft.Practices.Unity;

namespace HVTApp.Modules.Reports
{
    [ModuleAccess(Role.Admin, Role.SalesManager)]
    public class ReportsModule : ModuleBase
    {
        public ReportsModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.RegisterViewForNavigation<SalesReportView>();
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<ReportsMenu>());
        }
    }
}