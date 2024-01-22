using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptByHead: DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.VerificationAcceptByHead;
        protected override string ConfirmationMessage => "Вы уверены, что хотите принять результаты проработки?";

        public DoStepCommandAcceptByHead(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationItem> GetEventServiceItems()
        {
            yield return new NotificationItem(Manager, Role.SalesManager, $"ТСП проработано: {ViewModel.Model}");
            yield return new NotificationItem(ViewModel.UserConstructor.Model, Role.Constructor, $"ТСП проверено руководителем: {ViewModel.Model}");
        }

        protected override void DoStepAction()
        {
            ViewModel.Statuses.Add(ScriptStep.VerificationAcceptByHead);
            ViewModel.Statuses.Add(ScriptStep.FinishByConstructor);
            ViewModel.SaveCommand.Execute();
            this.RaiseCanExecuteChanged();
        }
    }
}