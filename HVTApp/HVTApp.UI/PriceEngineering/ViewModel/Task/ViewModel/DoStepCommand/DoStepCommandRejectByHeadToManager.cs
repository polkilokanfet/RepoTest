using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToManager : DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.RejectByHead;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи?";

        public DoStepCommandRejectByHeadToManager(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = EventServiceActionType.PriceEngineeringTaskRejectByHeadToManager,
                RecipientRole = Role.SalesManager,
                RecipientUser = Manager,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        protected override bool CanExecuteMethod()
        {
            return this.ViewModel.Model.UserConstructor == null && 
                   base.CanExecuteMethod();
        }
    }
}