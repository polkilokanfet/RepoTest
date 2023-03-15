using System;
using System.Linq;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestFinish : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestFinish;

        protected override string ConfirmationMessage => "�� �������, ��� ������� ������������?";

        public DoStepCommandProductionRequestFinish(TaskViewModel viewModel, IUnityContainer container, Action doAfter) : base(viewModel, container, doAfter)
        {
        }

        protected override void DoStepAction()
        {
            var vm = (TaskViewModelPlanMaker) ViewModel;
            var now = DateTime.Now;
            vm.SignalToStartProductionDone = now;
            vm.Messenger.SendMessage($"������ �/� {vm.Order.Number}");
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