using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandRejectByHeadToConstructor: DoStepCommandBase
    {
        protected override ScriptStep2 Step => ScriptStep2.VerificationRejectByHead;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ��������� ������ �� ��������� �����������?";

        public DoStepCommandRejectByHeadToConstructor(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}