using System.Collections.Generic;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToConstructor: DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.VerificationRejectByHead;

        protected override string ConfirmationMessage => "Вы уверены, что хотите отправить задачу на доработку исполнителю?";
        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            yield return new NotificationAboutPriceEngineeringTaskEventArg.RejectByHeadToConstructorManager(ViewModel.Model, Manager);
            yield return new NotificationAboutPriceEngineeringTaskEventArg.RejectByHeadToConstructor(ViewModel.Model);
        }

        public DoStepCommandRejectByHeadToConstructor(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}