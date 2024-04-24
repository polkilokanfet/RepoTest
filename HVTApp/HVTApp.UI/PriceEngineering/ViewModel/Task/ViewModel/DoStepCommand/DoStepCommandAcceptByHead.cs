using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptByHead: DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.VerificationAcceptByDesignDepartment;
        protected override string ConfirmationMessage => "Вы уверены, что хотите принять результаты проработки?";

        public DoStepCommandAcceptByHead(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead,
                RecipientRole = Role.SalesManager,
                RecipientUser = Manager,
                TargetEntityId = ViewModel.Model.Id
            };

            yield return new NotificationUnit
            {
                ActionType = NotificationActionType.PriceEngineeringTaskVerificationAcceptedByHead,
                RecipientRole = Role.Constructor,
                RecipientUser = ViewModel.Model.UserConstructor,
                TargetEntityId = ViewModel.Model.Id
            };
        }

        protected override void DoStepAction()
        {
            ViewModel.Statuses.Add(ScriptStep.VerificationAcceptByDesignDepartment);
            ViewModel.Statuses.Add(ScriptStep.FinishByConstructor);
            ViewModel.SaveCommand.Execute();
            this.RaiseCanExecuteChanged();
        }
    }
}