using System;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Infrastructure.Prism;
using HVTApp.Modules.DirectumLite.Menus;
using HVTApp.UI.Modules.Directum;
using Microsoft.Practices.Unity;
using Prism.Regions;
using HVTApp.Model;

namespace HVTApp.Modules.DirectumLite
{
    [ModuleAccess(Role.Admin, Role.SalesManager, Role.Economist, Role.Director, Role.Pricer, Role.PlanMaker, Role.ReportMaker)]
    public class DirectumLiteModule : ModuleBase
    {
        public DirectumLiteModule(IRegionManager regionManager, IUnityContainer container) : base(container, regionManager)
        {
        }

        protected override void RegisterTypes()
        {
            Container.Resolve<IDialogService>().RegisterShow<DirectumTasksIncomingViewModel, DirectumTasksIncomingToLateView>();

            //проверка на просроченные задания
            var viewModel = new DirectumTasksIncomingViewModel(Container);
            var items = viewModel.Items.ToList();
            viewModel.Items.Clear();
            viewModel.Items.AddRange(items.Where(x => x.FinishPlan < DateTime.Now || (x.Performer.Id == GlobalAppProperties.User.Id && !x.StartPerformer.HasValue)));
            if (viewModel.Items.Any())
            {
                Container.Resolve<IDialogService>().Show(viewModel, "Новые задачи и задачи с истекшим сроком исполнения/проверки");
            }
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<DirectumLiteMenu>());
        }
    }
}