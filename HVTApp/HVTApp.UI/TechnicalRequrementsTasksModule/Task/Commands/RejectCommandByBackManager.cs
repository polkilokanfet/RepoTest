using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RejectCommandByBackManager : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public RejectCommandByBackManager(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.HistoryElementWrapper.Comment))
            {
                MessageService.Message("Информация", "Перед отклонением необходимо заполнить комментарий (причину отклонения)");
                return;
            }

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Reject;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterRejectTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.HistoryElementWrapper = null;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsStarted &&
                   !ViewModel.IsRejected &&
                   !ViewModel.IsFinished;
        }
    }
}