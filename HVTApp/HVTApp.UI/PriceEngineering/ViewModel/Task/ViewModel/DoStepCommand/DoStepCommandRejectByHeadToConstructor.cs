using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToConstructor: DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.VerificationRejectByHead;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отправить задачу на доработку исполнителю?";

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskVerificationRejectedByHead,
                RecipientRole = Role.Constructor,
                RecipientUser = ViewModel.Model.UserConstructor,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        public DoStepCommandRejectByHeadToConstructor(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}