using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.Events.EventServiceEvents;
using HVTApp.Model.Events.EventServiceEvents.Args;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public abstract class DoStepCommand : DelegateLogCommand
    {
        public IUnityContainer Container { get; }
        protected readonly TaskViewModel ViewModel;
        private readonly Action _doAfterAction;
        protected readonly IMessageService MessageService;
        protected readonly IEventAggregator EventAggregator;
        private bool _showConfirmation = true;

        protected abstract ScriptStep Step { get; }
        protected abstract string ConfirmationMessage { get; }

        #region ctors

        protected DoStepCommand(TaskViewModel viewModel, IUnityContainer container, Action doAfterAction = null)
        {
            Container = container;
            ViewModel = viewModel;
            MessageService = container.Resolve<IMessageService>();
            EventAggregator = container.Resolve<IEventAggregator>();
            _doAfterAction = doAfterAction;
        }

        #endregion

        protected override void ExecuteMethod()
        {
            if (_showConfirmation)
            {
                var dr = MessageService.ShowYesNoMessageDialog("Подтверждение", ConfirmationMessage, defaultNo: true);
                if (dr != MessageDialogResult.Yes)
                {
                    return;
                }
            }

            this.DoStepAction();
            this.EventAggregator.GetEvent<PriceEngineeringTaskNotificationEvent>().Publish(new NotificationArgsPriceEngineeringTask(this.ViewModel.Model, this.GetEventServiceItems()));
            _doAfterAction?.Invoke();
        }

        protected abstract IEnumerable<NotificationArgsItem> GetEventServiceItems();

        protected virtual void DoStepAction()
        {
            ViewModel.Statuses.Add(this.Step, this.GetStatusComment());
            ViewModel.SaveCommand.Execute();
            this.RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Выполнить команду без подтверждения от пользователя
        /// </summary>
        public void ExecuteWithoutConfirmation()
        {
            _showConfirmation = false;
            base.Execute();
            _showConfirmation = true;
        }

        protected override bool CanExecuteMethod()
        {
            return ViewModel.IsValid && 
                   Step.AllowDoStep(ViewModel.Status);
        }

        protected virtual string GetStatusComment()
        {
            return null;
        }
    }
}