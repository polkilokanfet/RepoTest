using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptedByManager : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.Accept;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ������� ���������� ������?";
        protected override IEnumerable<NotificationItem> GetEventServiceItems()
        {
            yield return new NotificationItem(ViewModel.UserConstructor, Role.Constructor, $"��� ������� ����������: {ViewModel.Model}");
        }

        public DoStepCommandAcceptedByManager(TaskViewModelManagerOld viewModel, IUnityContainer container, Action doAfterAction) : base(viewModel, container, doAfterAction)
        {
        }
    }
}