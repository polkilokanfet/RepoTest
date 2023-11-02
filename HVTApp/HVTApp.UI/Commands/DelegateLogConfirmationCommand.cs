using System;
using HVTApp.Infrastructure.Services;

namespace HVTApp.UI.Commands
{
    public class DelegateLogConfirmationCommand : DelegateLogCommand
    {
        private const string DefaultConfirmationMessage = "Вы уверены?";

        private bool _showConfirmation = true;

        protected virtual IMessageService MessageService { get; }
        protected virtual string ConfirmationMessage { get; }

        #region ctors

        public DelegateLogConfirmationCommand(IMessageService messageService, string confirmationMessage, Action executeMethod, Func<bool> canExecuteMethod) 
            : base(executeMethod, canExecuteMethod)
        {
            MessageService = messageService;
            ConfirmationMessage = confirmationMessage;
        }

        public DelegateLogConfirmationCommand(IMessageService messageService, string confirmationMessage, Action executeMethod)
            : this(messageService, confirmationMessage, executeMethod, () => true)
        {
        }

        public DelegateLogConfirmationCommand(IMessageService messageService, Action executeMethod)
            : this(messageService, DefaultConfirmationMessage, executeMethod, () => true)
        {
        }

        public DelegateLogConfirmationCommand(IMessageService messageService, Action executeMethod, Func<bool> canExecuteMethod)
            : this(messageService, DefaultConfirmationMessage, executeMethod, canExecuteMethod)
        {
        }

        #endregion
        
        protected override void ExecuteMethod()
        {
            if (_showConfirmation)
            {
                var dr = MessageService.ConfirmationDialog("Подтверждение намерений", ConfirmationMessage, defaultNo: true);
                if (dr == false) return;
            }

            base.ExecuteMethod();
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
    }
}