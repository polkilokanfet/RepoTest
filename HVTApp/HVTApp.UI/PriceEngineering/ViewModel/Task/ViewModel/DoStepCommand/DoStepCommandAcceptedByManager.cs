using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptedByManager : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.Accept;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ������� ���������� ������?";
        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            yield return new NotificationArgsItem(ViewModel.UserConstructor, Role.Constructor, $"��� ������� ����������: {ViewModel.Model}");
        }

        public DoStepCommandAcceptedByManager(TaskViewModel viewModel, IUnityContainer container, Action doAfterAction) : base(viewModel, container, doAfterAction)
        {
        }
    }
}