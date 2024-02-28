using System;
using System.Collections.Generic;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceCalculations.ViewModel.PriceCalculation1.Commands
{
    public abstract class BaseBasePriceCalculationCommandNotifyCommand : DelegateLogCommand
    {
        protected readonly PriceCalculationViewModel ViewModel;
        protected readonly IUnityContainer Container;

        protected abstract string ConfirmationMessage { get; }
        protected abstract PriceCalculationHistoryItemType HistoryItemType { get; }

        protected BaseBasePriceCalculationCommandNotifyCommand(PriceCalculationViewModel viewModel, IUnityContainer container)
        {
            ViewModel = viewModel;
            Container = container;
        }

        protected bool DialogResult = false;

        protected override void ExecuteMethod()
        {
            DialogResult = Container.Resolve<IMessageService>().ConfirmationDialog(this.ConfirmationMessage, defaultYes: true);
            if (DialogResult == false) return;

            var historyItemWrapper = ViewModel.HistoryItem;
            historyItemWrapper.Moment = DateTime.Now;
            historyItemWrapper.Type = this.HistoryItemType;
            ViewModel.PriceCalculationWrapper.History.Add(historyItemWrapper);

            ViewModel.SaveCommand.Execute();

            foreach (var notificationUnit in GetNotificationUnits())
            {
                Container.Resolve<IEventAggregator>().GetEvent<NotificationEvent>().Publish(notificationUnit);
            }

            Container.Resolve<IEventAggregator>().GetEvent<AfterSavePriceCalculationEvent>().Publish(ViewModel.PriceCalculationWrapper.Model);

            ViewModel.RefreshCommands();

            ViewModel.GenerateNewHistoryItem();
        }

        protected abstract IEnumerable<NotificationUnit> GetNotificationUnits();

    }
}