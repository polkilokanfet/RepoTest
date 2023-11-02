using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class RejectCommandByFrontManager : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public RejectCommandByFrontManager(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (string.IsNullOrWhiteSpace(ViewModel.HistoryElementWrapper.Comment))
            {
                MessageService.Message("Информация", "Перед отклонением необходимо заполнить комментарий (причину отклонения)");
                return;
            }

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.RejectByFrontManager;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterRejectByFrontManagerTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.HistoryElementWrapper = null;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsFinished &&
                   !ViewModel.IsAccepted;
        }
    }
}