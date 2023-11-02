using System;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
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
            if (MessageService.ConfirmationDialog("Остановка задачи", "Вы уверены, что хотите остановить задачу?") == false)
                return;

            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Stop;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);
            ViewModel.TechnicalRequrementsTaskWrapper.DesiredFinishDate = null;

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterStopTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.SetNewHistoryElement();
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid &&
                   ViewModel.IsStarted && 
                   !ViewModel.IsStopped;
        }
    }
}