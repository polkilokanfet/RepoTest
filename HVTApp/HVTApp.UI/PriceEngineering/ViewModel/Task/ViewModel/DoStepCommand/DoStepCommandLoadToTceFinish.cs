using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandLoadToTceFinish : DoStepCommand
    {
        private readonly Action _doAfter;
        protected override ScriptStep Step => ScriptStep.LoadToTceFinish;

        protected override string ConfirmationMessage => "Вы уверены, что загрузили результаты проработки в Team Center?";

        public DoStepCommandLoadToTceFinish(TaskViewModelBackManager viewModel, IUnityContainer container, Action doAfter) : base(viewModel, container)
        {
            _doAfter = doAfter;
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            var tasks = ViewModel.Model.GetPriceEngineeringTasks(Container.Resolve<IUnitOfWork>());
            yield return new NotificationArgsItem(tasks.UserManager, Role.SalesManager, $"ТСП загружено в TeamCenter: {ViewModel.Model}");
        }

        protected override void BeforeDoStepAction()
        {
            var moment = DateTime.Now;
            var tasks = ViewModel.Model.ChildPriceEngineeringTasks
                .SelectMany(task => task.GetAllPriceEngineeringTasks())
                .Where(task => task.Status.Equals(ScriptStep.LoadToTceStart));
            foreach (var task in tasks)
            {
                task.Statuses.Add(new PriceEngineeringTaskStatus
                {
                    Moment = moment,
                    StatusEnum = ScriptStep.LoadToTceFinish.Value
                });
            }
        }

        protected override void DoStepAction()
        {
            base.DoStepAction();
            _doAfter?.Invoke();
        }

        protected override string GetStatusComment()
        {
            var vm = (TaskViewModelBackManager)ViewModel;
            var sb = new StringBuilder();
            sb.AppendLine($"Заявка в TeamCenter: {vm.TasksWrapperBackManager.TceNumber}");
            foreach (var sccVersion in vm.TasksTceItem.SccVersions.Where(x => x.IsActual))
            {
                sb.AppendLine($" - {sccVersion.Name}: [{sccVersion.OriginalStructureCostNumber} => {sccVersion.Version}]");
            }

            return sb.ToString();
        }

        protected override bool CanExecuteMethod()
        {
            var vm = (TaskViewModelBackManager) ViewModel;
            return vm.TasksWrapperBackManager.IsValid && 
                   vm.TasksTceItem.IsValid &&
                   ViewModel.Status.Equals(ScriptStep.LoadToTceStart);
        }
    }
}