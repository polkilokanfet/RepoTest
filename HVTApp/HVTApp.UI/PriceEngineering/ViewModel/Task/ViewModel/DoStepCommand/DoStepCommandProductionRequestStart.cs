using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public class DoStepCommandProductionRequestStart : DoStepCommandBase
    {
        protected override ScriptStep Step => ScriptStep.ProductionRequestStart;

        protected override string ConfirmationMessage => "�� �������, ��� ������ ������� ������������?";

        public DoStepCommandProductionRequestStart(TaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void DoStepAction()
        {
            //�������� �� ���������� �����
            var tasks = this.ViewModel.Model.GetAllPriceEngineeringTasks().ToList();
            var notAccepted = tasks.Where(task => Step.PossiblePreviousSteps.Contains(task.Status) == false).ToList();
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

            //�������� �� ������� �/�
            if (this.ViewModel.Model.SalesUnits.Any(salesUnit => salesUnit.Order != null))
            {
                MessageService.ShowOkMessageDialog("�����", "� ������� ������������ ��� ���� ������� �/�");
                return;
            }

            base.DoStepAction();
        }
    }
}