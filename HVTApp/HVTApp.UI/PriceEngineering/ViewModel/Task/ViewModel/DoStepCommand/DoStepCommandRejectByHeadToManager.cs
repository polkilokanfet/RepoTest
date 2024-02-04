using System.Collections.Generic;
using HVTApp.Model.Events.EventServiceEvents.Args;
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

        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            yield return new NotificationAboutPriceEngineeringTaskEventArg.RejectByHeadToManager(ViewModel.Model, Manager);
        }

        protected override bool CanExecuteMethod()
        {
            return this.ViewModel.Model.UserConstructor == null && 
                   base.CanExecuteMethod();
        }
    }
}