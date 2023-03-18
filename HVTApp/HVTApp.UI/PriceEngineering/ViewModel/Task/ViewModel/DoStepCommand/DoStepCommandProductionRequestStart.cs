using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestStart : DoStepCommand
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStart;

        protected override string ConfirmationMessage => "Вы уверены, что хотите открыть производство?";

        public DoStepCommandProductionRequestStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            foreach (var user in Container.Resolve<IUnitOfWork>().Repository<User>().Find(x => x.Roles.Any(r => r.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationArgsItem(user, Role.BackManagerBoss, $"Назначьте плановика: {ViewModel.Model}");
            }
        }

        protected override void DoStepAction()
        {
            //проверка на наличие з/з
            if (this.ViewModel.Model.SalesUnits.Any(salesUnit => salesUnit.SignalToStartProduction.HasValue))
            {
                MessageService.ShowOkMessageDialog("Отказ", "В перечне оборудования уже есть позиции с запросом на открытие з/з");
                return;
            }

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

            var now = DateTime.Now;
            foreach (var salesUnit in ViewModel.SalesUnits)
            {
                salesUnit.SignalToStartProduction = now;
            }

            base.DoStepAction();
        }
    }
}