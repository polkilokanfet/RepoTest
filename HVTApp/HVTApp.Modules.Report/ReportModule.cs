using System.ComponentModel;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.Report.Menus;
using Microsoft.Practices.Unity;
using Prism.Regions;

namespace HVTApp.Modules.Report
{
    [ModuleAccess(Role.Admin, Role.SalesManager)]
    public class ReportModule : ModuleBase
    {
        public ReportModule(IUnityContainer container, IRegionManager regionManager) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<ReportMenu>());
        }
    }
}