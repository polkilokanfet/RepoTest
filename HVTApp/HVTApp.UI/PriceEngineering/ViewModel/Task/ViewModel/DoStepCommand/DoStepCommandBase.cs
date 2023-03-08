using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.UI.PriceEngineering.DoStepCommand
{
    public abstract class DoStepCommandBase : DelegateLogCommand
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

        protected DoStepCommandBase(TaskViewModel viewModel, IUnityContainer container, Action doAfterAction = null)
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
            _doAfterAction?.Invoke();
        }

        protected virtual void DoStepAction()
        {
            ViewModel.Statuses.Add(Step);
            ViewModel.AcceptChanges();
            ViewModel.SaveCommand.Execute();
            Step.PublishEvent(EventAggregator, ViewModel.Model);
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
    }
}