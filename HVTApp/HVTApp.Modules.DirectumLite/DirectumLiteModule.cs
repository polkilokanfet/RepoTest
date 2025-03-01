using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extensions;
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
            Container.Resolve<IDialogService>().Register<DirectumTasksIncomingViewModel, DirectumTasksIncomingToLateView>();
            ShowDirectumTasks();
        }

        /// <summary>
        /// Показ просроченных заданий
        /// </summary>
        private void ShowDirectumTasks()
        {
            var viewModel = new DirectumTasksIncomingViewModel(Container);
            viewModel.LoadComplited +=
                () =>
                {
                    //просроченные задачи
                    var targetItems = viewModel.Items
                        .Where(directumTask => directumTask.IsActual)
                        .Where(directumTask =>
                            (directumTask.Group.Author.Id == GlobalAppProperties.User.Id && directumTask.FinishPlan < DateTime.Now) ||     //если пользователь автор задачи и задача просрочена
                            (directumTask.Performer.Id == GlobalAppProperties.User.Id && !directumTask.StartPerformer.HasValue) ||         //если пользователь исполнитель и не приступил к исполнению задачи
                            (directumTask.Performer.Id == GlobalAppProperties.User.Id && !directumTask.FinishPerformer.HasValue && directumTask.FinishPlan < DateTime.Now))          //если пользователь исполнитель и просрочил выполнение
                        .ToList();

                    if (targetItems.Any())
                    {
                        viewModel.Items.Clear();
                        viewModel.Items.AddRange(targetItems);
                        Container.Resolve<IDialogService>().Show(viewModel, "Новые задачи и задачи с истекшим сроком исполнения/проверки");
                    }
                };
        }

        protected override void ResolveOutlookGroup()
        {
            RegionManager.Regions[RegionNames.OutlookBarGroupsRegion].Add(Container.Resolve<DirectumLiteMenu>());
        }
    }
}