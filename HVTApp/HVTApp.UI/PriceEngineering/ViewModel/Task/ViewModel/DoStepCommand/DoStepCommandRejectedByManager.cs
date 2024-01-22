using System.Collections.Generic;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByManager : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.RejectByManager;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ���������� ������ �����������?";
        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            yield return new NotificationAboutPriceEngineeringTaskEventArg.RejectedByManager(ViewModel.Model);
        }

        public DoStepCommandRejectedByManager(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}