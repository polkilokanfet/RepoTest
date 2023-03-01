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
        protected readonly TaskViewModel ViewModel;
        private readonly IMessageService _messageService;
        private readonly IEventAggregator _eventAggregator;
        private bool _showConfirmation = true;

        protected abstract ScriptStep2 Step { get; }
        protected abstract string ConfirmationMessage { get; }

        #region ctors

        protected DoStepCommandBase(TaskViewModel viewModel, IUnityContainer container)
        {
            ViewModel = viewModel;
            _messageService = container.Resolve<IMessageService>();
            _eventAggregator = container.Resolve<IEventAggregator>();
        }

        #endregion

        protected override void ExecuteMethod()
        {
            if (_showConfirmation)
            {
                var dr = _messageService.ShowYesNoMessageDialog("ѕодтвердите свои намерени€", ConfirmationMessage, defaultNo: true);
                if (dr != MessageDialogResult.Yes)
                {
                    return;
                }
            }

            this.DoStepAction();
        }

        protected virtual void DoStepAction()
        {
            ViewModel.Statuses.Add(Step);
            ViewModel.SaveCommand.Execute();
            Step.PublishEvent(_eventAggregator, ViewModel.Model);
        }

        /// <summary>
        /// ¬ыполнить команду без подтверждени€ от пользовател€
        /// </summary>
        public void ExecuteWithoutConfirmation()
        {
            _showConfirmation = false;
            base.Execute();
            _showConfirmation = true;
        }

        protected override bool CanExecuteMethod()
        {
            return Step.AllowDoStep(ViewModel.Status);
        }
    }
}