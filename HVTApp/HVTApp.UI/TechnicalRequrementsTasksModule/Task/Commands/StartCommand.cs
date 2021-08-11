using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class StartCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public StartCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (MessageService.ShowYesNoMessageDialog("", "¬ы уверены, что хотите запустить задачу?") != MessageDialogResult.Yes)
                return;

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Start;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterStartTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);
        }

        protected override bool CanExecuteMethod()
        {
            return !ViewModel.IsStarted;
        }
    }
}