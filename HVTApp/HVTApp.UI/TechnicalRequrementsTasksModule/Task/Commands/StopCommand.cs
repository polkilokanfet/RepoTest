using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class StopCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public StopCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (MessageService.ShowYesNoMessageDialog("", "¬ы уверены, что хотите остановить задачу?") != MessageDialogResult.Yes)
                return;

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Stop;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterStopTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.HistoryElementWrapper = new TechnicalRequrementsTaskHistoryElementWrapper(new TechnicalRequrementsTaskHistoryElement());
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsStarted && 
                   !ViewModel.IsStopped;
        }
    }
}