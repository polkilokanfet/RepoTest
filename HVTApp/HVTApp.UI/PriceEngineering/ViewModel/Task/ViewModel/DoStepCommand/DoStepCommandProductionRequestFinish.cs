using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestFinish : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "�� �������, ��� ������� ������������?";

        public DoStepCommandProductionRequestFinish(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}