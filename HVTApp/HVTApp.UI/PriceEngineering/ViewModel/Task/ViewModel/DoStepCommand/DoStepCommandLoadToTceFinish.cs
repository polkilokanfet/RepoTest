using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandLoadToTceFinish : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.LoadToTceFinish;

        protected override string ConfirmationMessage => "Вы уверены, что загрузили результаты проработки в ТС?";

        public DoStepCommandLoadToTceFinish(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }
    }
}