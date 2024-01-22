using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToManager : DoStepCommand<TaskViewModelDesignDepartmentHead>
    {
        protected override ScriptStep Step => ScriptStep.RejectByHead;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ���������� ������?";

        public DoStepCommandRejectByHeadToManager(TaskViewModelDesignDepartmentHead viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationItem> GetEventServiceItems()
        {
            yield return new NotificationItem(Manager, Role.SalesManager, $"������������ �� �������� ���� ���: {ViewModel.Model}");
        }

        protected override bool CanExecuteMethod()
        {
            return this.ViewModel.Model.UserConstructor == null && 
                   base.CanExecuteMethod();
        }
    }
}