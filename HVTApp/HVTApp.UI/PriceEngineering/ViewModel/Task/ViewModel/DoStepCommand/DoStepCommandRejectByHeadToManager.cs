using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToManager : DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.RejectByHead;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отклонить проработку задачи менеджеру?";

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskRejectByHeadToManager,
                RecipientRole = Role.SalesManager,
                RecipientUser = Manager,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        public DoStepCommandRejectByHeadToManager(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override bool CanExecuteMethod()
        {
            return this.ViewModel.Model.UserConstructor == null && 
                   base.CanExecuteMethod();
        }
    }
}