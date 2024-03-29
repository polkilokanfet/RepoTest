using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Model.Events.NotificationArgs;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestStart : DoStepCommand<TaskViewModelManagerOld>
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStart;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ������� ������������?";

        public DoStepCommandProductionRequestStart(TaskViewModelManagerOld viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override IEnumerable<NotificationAboutPriceEngineeringTaskEventArg> GetNotificationsArgs()
        {
            foreach (var user in UnitOfWork.Repository<User>().Find(user => user.Roles.Any(role => role.Role == Role.BackManagerBoss)))
            {
                yield return new NotificationAboutPriceEngineeringTaskEventArg.ProductionRequestStartBackManagerBoss(this.ViewModel.Model, user);
            }
        }

        protected override bool NeedAddSameStatusOnSubTasks => true;

        protected override bool AllowDoStepAction()
        {
            var priceEngineeringTask = UnitOfWork.Repository<PriceEngineeringTask>().GetById(ViewModel.Model.Id);

            //�������� �� ������� �/�
            if (priceEngineeringTask.SalesUnits.Any(salesUnit => salesUnit.SignalToStartProduction.HasValue))
            {
                MessageService.Message("�����", "� ������� ������������ ��� ���� ������� � �������� �� �������� �/�");
                return false;
            }

            //�������� �� ���������� �����
            var tasks = priceEngineeringTask.GetAllPriceEngineeringTasks().ToList();
            var possiblePreviousSteps = Step.PossiblePreviousSteps.Union(new[] {ScriptStep.ProductionRequestStart});
            var notAccepted = tasks.Where(task => possiblePreviousSteps.Contains(task.Status) == false).ToList();
            if (notAccepted.Any())
            {
                MessageService.Message("�����", $"������� ������� �����:\n{notAccepted.Select(task => task.ProductBlock).ToStringEnum()}");
                return false;
            }

            //�������� �� ���������� ��� ������������
            var notValidForProduction = tasks.Where(task => task.IsValidForProduction == false).ToList();
            if (notValidForProduction.Any())
            {
                MessageService.Message("�����", $"������� ������������ �� � ������:\n{notValidForProduction.Select(task => task.ProductBlock).ToStringEnum()}");
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