using System;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
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

        protected override void DoStepAction()
        {
            var steps = new [] {ScriptStep.Accept, ScriptStep.LoadToTceStart, ScriptStep.LoadToTceFinish};
            var tasks = this.ViewModel.Model.GetAllPriceEngineeringTasks().ToList();
            var notAccepted = tasks.Where(task => steps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any())
            {
                MessageService.ShowOkMessageDialog("Отказ", $"Сначала примите блоки:\n{notAccepted.Select(task => task.ProductBlock).ToStringEnum()}");
                return;
            }

            var now = DateTime.Now;
            foreach (var salesUnit in ViewModel.Model.SalesUnits)
            {
                salesUnit.SignalToStartProduction = now;
            }

            foreach (var childPriceEngineeringTask in this.ViewModel.ChildPriceEngineeringTasks)
            {
                if (childPriceEngineeringTask is TaskViewModelManagerOld task)
                {
                    if (task.LoadToTceStartCommand.CanExecute(null))
                    {
                        ((DoStepCommandLoadToTceStart)task.LoadToTceStartCommand).ExecuteWithoutConfirmation();
                    }
                }
                else
                {
                    throw new ArgumentException("Неверный тип задачи");
                }
            }

            base.DoStepAction();
        }

        protected override bool CanExecuteMethod()
        {
            return 
                !this.ViewModel.Model.GetAllPriceEngineeringTasks().All(task => ScriptStep.LoadToTceFinish.Equals(ViewModel.Model.Status)) && 
                base.CanExecuteMethod();
        }
    }
}