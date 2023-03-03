using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectedByManager : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.RejectByManager;
        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ���������� ������?";

        public DoStepCommandRejectedByManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}