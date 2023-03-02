using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToManager : DoStepCommandBase
    {
        protected override ScriptStep2 Step => ScriptStep2.RejectByHead;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ���������� ������?";

        public DoStepCommandRejectByHeadToManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}