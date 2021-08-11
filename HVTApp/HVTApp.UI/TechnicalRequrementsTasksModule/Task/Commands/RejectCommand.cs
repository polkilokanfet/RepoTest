using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RejectCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public RejectCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.HistoryElementWrapper.Comment))
            {
                MessageService.ShowOkMessageDialog("Информация", "Перед отклонением необходимо заполнить комментарий (причину отклонения)");
                return;
            }

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Reject;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterRejectTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsStarted &&
                   !ViewModel.IsRejected &&
                   !ViewModel.IsFinished;
        }
    }
}