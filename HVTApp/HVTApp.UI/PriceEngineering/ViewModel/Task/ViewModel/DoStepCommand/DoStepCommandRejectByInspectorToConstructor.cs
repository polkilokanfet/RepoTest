using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByInspectorToConstructor: DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.VerificationReject;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отправить задачу на доработку исполнителю?";

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskVerificationRejected,
                RecipientRole = Role.Constructor,
                RecipientUser = ViewModel.Model.UserConstructor,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        public DoStepCommandRejectByInspectorToConstructor(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}