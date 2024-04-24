using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Model.POCOs;
using HVTApp.UI.PriceEngineering.Wrapper;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public abstract class DoStepCommandRejectVerificationBase<TViewModel>: DoStepCommand<TViewModel> 
        where TViewModel : TaskViewModelBaseInspector
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

        protected DoStepCommandRejectVerificationBase(TViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }

    public class DoStepCommandRejectVerificationByDesignDepartmentHead : DoStepCommandRejectVerificationBase<TaskViewModelDesignDepartmentHead>
    {
        public DoStepCommandRejectVerificationByDesignDepartmentHead(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string GetStatusComment()
        {
            return $"Проверяющий: {this.ViewModel.Model.DesignDepartment.Head}";
        }
    }

    public class DoStepCommandRejectVerificationByInspector : DoStepCommandRejectVerificationBase<TaskViewModelInspector>
    {
        public DoStepCommandRejectVerificationByInspector(TaskViewModelInspector viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override string GetStatusComment()
        {
            return $"Проверяющий: {this.ViewModel.Model.DesignDepartment.Head}";
        }
    }

}