using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStopByManager : DoStepCommandBase
    {
        protected override ScriptStep2 Step => ScriptStep2.Stop;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ���������� ���������� ������?";

        public DoStepCommandStopByManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}