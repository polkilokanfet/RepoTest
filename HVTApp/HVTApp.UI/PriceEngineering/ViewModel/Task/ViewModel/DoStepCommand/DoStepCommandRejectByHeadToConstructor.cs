using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToConstructor: DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.VerificationRejectByHead;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ������ �� ��������� �����������?";
        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetEventServiceItems()
        {
            yield return new NotificationAboutPriceEngineeringTaskEventArg(this.ViewModel.Model, Manager, Role.SalesManager, $"��� ������� ���������: {ViewModel.Model}");
            yield return new NotificationAboutPriceEngineeringTaskEventArg(this.ViewModel.Model, ViewModel.UserConstructor.Model, Role.Constructor, $"��� ������� ���������: {ViewModel.Model}");
        }

        public DoStepCommandRejectByHeadToConstructor(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}