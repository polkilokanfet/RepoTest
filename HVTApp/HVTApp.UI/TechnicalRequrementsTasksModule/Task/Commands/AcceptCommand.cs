using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public class AcceptCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        public AcceptCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            var msg = "¬ы уверены, что хотите прин€ть проработку задачи?";
            if (MessageService.ShowYesNoMessageDialog("", msg) != MessageDialogResult.Yes)
                return;

            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.HistoryElementWrapper.Type = TechnicalRequrementsTaskHistoryElementType.Accept;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            Container.Resolve<IEventAggregator>().GetEvent<AfterAcceptTechnicalRequrementsTaskEvent>().Publish(ViewModel.TechnicalRequrementsTaskWrapper.Model);

            ViewModel.SetNewHistoryElement();
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsFinished &&
                   !ViewModel.IsAccepted;
        }
    }
}