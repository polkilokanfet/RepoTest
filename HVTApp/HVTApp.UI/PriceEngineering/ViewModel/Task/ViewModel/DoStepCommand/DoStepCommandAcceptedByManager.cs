using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandAcceptedByManager : DoStepCommandBase
    {
        protected override ScriptStep2 Step => ScriptStep2.Accept;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ������� ���������� ������?";

        public DoStepCommandAcceptedByManager(TaskViewModel viewModel, IUnityContainer container, Action doAfterAction) : base(viewModel, container, doAfterAction)
        {
        }
    }
}