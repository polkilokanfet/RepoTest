using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandLoadToTceStart : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.LoadToTceStart;

        protected override string ConfirmationMessage => "Вы уверены, что хотите загрузить результаты проработки в ТС?";

        public DoStepCommandLoadToTceStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}