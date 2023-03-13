using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandStopByManager : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.Stop;

        protected override string ConfirmationMessage => "Вы уверены, что хотите остановить проработку задачи?";

        public DoStepCommandStopByManager(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            foreach (var salesUnit in ViewModel.SalesUnits)
            {
                salesUnit.SignalToStartProduction = null;
                salesUnit.SignalToStartProductionDone = null;
            }
            base.DoStepAction();
        }
    }
}