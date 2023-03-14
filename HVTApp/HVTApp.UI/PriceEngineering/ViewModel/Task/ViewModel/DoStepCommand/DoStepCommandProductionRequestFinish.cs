using System;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.ObjectBuilder2;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestFinish : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "Вы уверены, что открыли производство?";

        public DoStepCommandProductionRequestFinish(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            var vm = (TaskViewModelPlanMaker) ViewModel;
            var now = DateTime.Now;
            vm.SalesUnits.ForEach(x => x.SignalToStartProductionDone = now);
            vm.Messenger.SendMessage($"Открыт з/з {vm.Order.Number}");
            foreach (var priceEngineeringTask in vm.Model.GetAllPriceEngineeringTasks().Where(x => x.Id != vm.Model.Id))
            {
                priceEngineeringTask.Statuses.Add(new PriceEngineeringTaskStatus
                {
                    Moment = now, 
                    StatusEnum = ScriptStep.ProductionRequestFinish.Value
                });
            }
            base.DoStepAction();
        }
    }
}