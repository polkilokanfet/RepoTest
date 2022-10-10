using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Services;

namespace HVTApp.UI.Commands
{
    public class DelegateLogConfirmationCommand : DelegateLogCommand
    {
        private readonly IMessageService _messageService;
        private readonly string _confirmationMessage;
        private bool _showConfirmation = true;

        public DelegateLogConfirmationCommand(IMessageService messageService, string confirmationMessage, Action executeMethod) : this(messageService, confirmationMessage, executeMethod, () => true)
        {
        }
        public DelegateLogConfirmationCommand(IMessageService messageService, string confirmationMessage, Action executeMethod, Func<bool> canExecuteMethod) : base(executeMethod, canExecuteMethod)
        {
            _messageService = messageService;
            _confirmationMessage = confirmationMessage;
        }
        
        protected override void ExecuteMethod()
        {
            if (_showConfirmation && _messageService.ShowYesNoMessageDialog("ѕодтвердите свои намерени€", _confirmationMessage, defaultNo:true) != MessageDialogResult.Yes)
            {
                return;
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