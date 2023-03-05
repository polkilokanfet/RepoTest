using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestStart : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStart;

        protected override string ConfirmationMessage => "¬ы уверены, что хотите открыть производство?";

        public DoStepCommandProductionRequestStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}