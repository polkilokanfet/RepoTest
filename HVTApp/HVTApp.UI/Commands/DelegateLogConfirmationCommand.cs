using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;

namespace HVTApp.UI.Commands
{
    public class DelegateLogConfirmationCommand : DelegateLogCommand
    {
        private readonly IMessageService _messageService;
        private bool _showConfirmation = true;

        protected virtual string ConfirmationMessage { get; set; }

        #region ctors

        public DelegateLogConfirmationCommand(IMessageService messageService, string confirmationMessage, Action executeMethod) 
            : this(messageService, confirmationMessage, executeMethod, () => true)
        {
        }

        public DelegateLogConfirmationCommand(IMessageService messageService, string confirmationMessage, Action executeMethod, Func<bool> canExecuteMethod) 
            : base(executeMethod, canExecuteMethod)
        {
            _messageService = messageService;
            ConfirmationMessage = confirmationMessage;
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

            base.ExecuteMethod();
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
    }
}