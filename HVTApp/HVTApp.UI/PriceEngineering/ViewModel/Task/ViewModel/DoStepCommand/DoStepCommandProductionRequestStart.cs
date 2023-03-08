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
            //проверка на непринятые блоки
            var tasks = this.ViewModel.Model.GetAllPriceEngineeringTasks().ToList();
            var notAccepted = tasks.Where(task => Step.PossiblePreviousSteps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any())
            {
                MessageService.ShowOkMessageDialog("Отказ", $"Сначала примите блоки:\n{notAccepted.Select(task => task.ProductBlock).ToStringEnum()}");
                return;
            }

            //проверка на валидность для производства
            var notValidForProduction = tasks.Where(task => task.IsValidForProduction == false).ToList();
            if (notValidForProduction.Any())
            {
                MessageService.ShowOkMessageDialog("Отказ", $"Сначала досогласуйте ТЗ в блоках:\n{notValidForProduction.Select(task => task.ProductBlock).ToStringEnum()}");
                return;
            }

            //проверка на наличие з/з
            if (this.ViewModel.Model.SalesUnits.Any(salesUnit => salesUnit.Order != null))
            {
                MessageService.ShowOkMessageDialog("Отказ", "В перечне оборудования уже есть отрытые з/з");
                return;
            }

            base.DoStepAction();
        }
    }
}