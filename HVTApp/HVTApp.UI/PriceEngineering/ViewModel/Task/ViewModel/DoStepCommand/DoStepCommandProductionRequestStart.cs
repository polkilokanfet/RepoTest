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

        protected override string ConfirmationMessage => "�� �������, ��� ������ ������� ������������?";

        public DoStepCommandProductionRequestStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationArgsItem> GetEventServiceItems()
        {
            foreach (var user in Container.Resolve<IUnitOfWork>().Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationArgsItem(user, Role.BackManagerBoss, $"��������� ���������: {ViewModel.Model}");
            }
        }

        protected override bool SetSameStatusOnSubTasks => true;

        protected override void DoStepAction()
        {
            var priceEngineeringTask = Container.Resolve<IUnitOfWork>().Repository<PriceEngineeringTask>().GetById(ViewModel.Model.Id);

            //�������� �� ������� �/�
            if (priceEngineeringTask.SalesUnits.Any(salesUnit => salesUnit.SignalToStartProduction.HasValue))
            {
                MessageService.ShowOkMessageDialog("�����", "� ������� ������������ ��� ���� ������� � �������� �� �������� �/�");
                return;
            }

            //�������� �� ���������� �����
            var tasks = priceEngineeringTask.GetAllPriceEngineeringTasks().ToList();
            var possiblePreviousSteps = Step.PossiblePreviousSteps.Union(new[] {ScriptStep.ProductionRequestStart});
            var notAccepted = tasks.Where(task => possiblePreviousSteps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any())
            {
                MessageService.ShowOkMessageDialog("�����", $"������� ������� �����:\n{notAccepted.Select(task => task.ProductBlock).ToStringEnum()}");
                return;
            }

            //�������� �� ���������� ��� ������������
            var notValidForProduction = tasks.Where(task => task.IsValidForProduction == false).ToList();
            if (notValidForProduction.Any())
            {
                MessageService.ShowOkMessageDialog("�����", $"������� ������������ �� � ������:\n{notValidForProduction.Select(task => task.ProductBlock).ToStringEnum()}");
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