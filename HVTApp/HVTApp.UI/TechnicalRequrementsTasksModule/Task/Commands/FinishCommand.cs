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
            //если требуется РТЗ
            if (ViewModel.TechnicalRequrementsTaskWrapper.LogisticsCalculationRequired &&
                !ViewModel.TechnicalRequrementsTaskWrapper.ShippingCostFiles.Any())
            {
                MessageService.ShowOkMessageDialog("Информация", "Добавьте в задачу расчет транспортных затрат.");
                return;
            }

            //если не вложены ответы конструкторов
            var msg = ViewModel.TechnicalRequrementsTaskWrapper.AnswerFiles.Any() 
                ? "Вы уверены, что хотите завершить проработку задачи?" 
                : "Вы не вложили ни один ответ конструкторов.\nВы уверены, что хотите завершить проработку задачи?";

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