using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestStart : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStart;

        protected override string ConfirmationMessage => "Вы уверены, что хотите открыть производство?";

        public DoStepCommandProductionRequestStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            var tasks = this.ViewModel.Model.GetAllPriceEngineeringTasks().ToList();
            var notAccepted = tasks.Where(task => Step.PossiblePreviousSteps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any())
            {
                MessageService.ShowOkMessageDialog("Отказ", $"Сначала примите блоки:\n{notAccepted.Select(x => x.ProductBlock).ToStringEnum()}");
                return;
            }

            base.DoStepAction();
        }
    }
}