using System;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class FinishCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public FinishCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            //���� ��������� ���
            if (ViewModel.TechnicalRequrementsTaskWrapper.LogisticsCalculationRequired &&
                !ViewModel.TechnicalRequrementsTaskWrapper.ShippingCostFiles.Any())
            {
                MessageService.ShowOkMessageDialog("����������", "�������� � ������ ������ ������������ ������.");
                return;
            }

            //���� �� ������� ������ �������������
            var msg = ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles.Any() 
                ? "�� �������, ��� ������ ��������� ���������� ������?" 
                : "�� �� ������� �� ���� ����� �������������.\n�� �������, ��� ������ ��������� ���������� ������?";

            if (MessageService.ShowYesNoMessageDialog("", msg) != MessageDialogResult.Yes)
                return;

            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Finish;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterFinishTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.HistoryElementWrapper = null;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted && 
                   !ViewModel.IsFinished && 
                   ViewModel.TechnicalRequrementsTaskWrapper.IsValid;
        }
    }
}