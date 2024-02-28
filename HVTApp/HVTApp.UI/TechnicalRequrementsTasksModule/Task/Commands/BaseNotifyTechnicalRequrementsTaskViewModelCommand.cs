using System;
using System.Collections.Generic;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.TechnicalRequrementsTasksModule
{
    public abstract class BaseNotifyTechnicalRequrementsTaskViewModelCommand : BaseTechnicalRequrementsTaskViewModelCommand
    {
        protected abstract string ConfirmationMessage { get; }
        protected abstract TechnicalRequrementsTaskHistoryElementType HistoryElementType { get; }

        protected BaseNotifyTechnicalRequrementsTaskViewModelCommand(TechnicalRequrementsTaskViewModel viewModel, IUnityContainer container) : base(viewModel, container)
        {
        }

        protected override void ExecuteMethod()
        {
            if (MessageService.ConfirmationDialog("Задача в ТСЕ", this.ConfirmationMessage) == false)
                return;

            ViewModel.HistoryElementWrapper.Type = this.HistoryElementType;
            ViewModel.HistoryElementWrapper.Moment = DateTime.Now;
            ViewModel.TechnicalRequrementsTaskWrapper.HistoryElements.Add(ViewModel.HistoryElementWrapper);

            ActionBeforeSave();

            ViewModel.SaveCommand.Execute();

            this.RaiseCanExecuteChanged();

            foreach (var notificationUnit in this.GetNotificationUnits())
            {
                Container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(notificationUnit);
            }

            ViewModel.SetNewHistoryElement();
        }

        protected virtual void ActionBeforeSave()
        {

        }

        protected abstract IEnumerable<NotificationUnit> GetNotificationUnits();
    }
}