using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandLoadToTceFinish : DoStepCommand<TaskViewModelBackManager>
    {
        private readonly Action _doAfter;
        protected override ScriptStep Step => ScriptStep.LoadToTceFinish;

        protected override string ConfirmationMessage => "Вы уверены, что загрузили результаты проработки в TeamCenter?";

        public DoStepCommandLoadToTceFinish(TaskViewModelBackManager viewModel, IUnityContainer container, Action doAfter) : base(viewModel, container)
        {
            _doAfter = doAfter;
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            yield return new NotificationArgsItem(Manager, Role.SalesManager, $"ТСП загружено в TeamCenter: {ViewModel.Model}");
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override void DoStepAction()
        {
            base.DoStepAction();
            _doAfter?.Invoke();
        }

        protected override string GetStatusComment()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Заявка в TeamCenter: {ViewModel.TasksWrapperBackManager.TceNumber}");
            foreach (var sccVersion in ViewModel.TasksTceItem.SccVersions.Where(scc => scc.IsActual))
            {
                sb.AppendLine($" - {sccVersion.Name}: [{sccVersion.OriginalStructureCostNumber} => {sccVersion.Version}]");
            }

            return sb.ToString();
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.TasksWrapperBackManager.IsValid && 
                   ViewModel.TasksTceItem.IsValid &&
                   ViewModel.Status.Equals(ScriptStep.LoadToTceStart);
        }
    }
}