using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Enums;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestStart : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStart;

        protected override string ConfirmationMessage => "Вы уверены, что хотите открыть производство?";

        public DoStepCommandProductionRequestStart(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationUnit> GetNotificationUnits()
        {
            foreach (var user in UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationUnit
                {
                    ActionType = NotificationActionType.PriceEngineeringTaskProductionRequestStart,
                    RecipientRole = Role.BackManagerBoss,
                    RecipientUser = user,
                    TargetEntityId = ViewModel.Model.Id
                };
            }
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override bool AllowDoStepAction()
        {
            var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(ViewModel.Model.Id);

            //проверка на наличие з/з
            if (priceEngineeringTask.SalesUnits.Any(salesUnit => salesUnit.SignalToStartProduction.HasValue))
            {
                MessageService.Message("Отказ", "В перечне оборудования уже есть позиции с запросом на открытие з/з");
                return false;
            }

            //проверка на непринятые блоки
            var tasks = priceEngineeringTask.GetAllPriceEngineeringTasks().ToList();
            var possiblePreviousSteps = Step.PossiblePreviousSteps.Union(new[] {ScriptStep.ProductionRequestStart});
            var notAccepted = tasks.Where(task => possiblePreviousSteps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any())
            {
                MessageService.Message("Отказ", $"Сначала примите блоки:\n{notAccepted.Select(task => task.ProductBlock).ToStringEnum()}");
                return false;
            }

            //проверка на валидность для производства
            var notValidForProduction = tasks.Where(task => task.IsValidForProduction == false).ToList();
            if (notValidForProduction.Any())
            {
                MessageService.Message("Отказ", $"Сначала досогласуйте ТЗ в блоках:\n{notValidForProduction.Select(task => task.ProductBlock).ToStringEnum()}");
                return false;
            }

            return true;
        }

        protected override void BeforeDoStepAction()
        {
            var now = DateTime.Now;
            ViewModel.SalesUnits.ForEach(x => x.SignalToStartProduction = now);
        }
    }
}