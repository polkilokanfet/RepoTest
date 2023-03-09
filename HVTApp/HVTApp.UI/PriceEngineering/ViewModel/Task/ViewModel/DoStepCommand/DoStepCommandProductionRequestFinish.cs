using System;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestFinish : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "¬ы уверены, что открыли производство?";

        public DoStepCommandProductionRequestFinish(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            var vm = (TaskViewModelBackManager) ViewModel;
            var now = DateTime.Now;
            vm.SalesUnits.ForEach(x => x.SignalToStartProductionDone = now);
            base.DoStepAction();
        }
    }
}