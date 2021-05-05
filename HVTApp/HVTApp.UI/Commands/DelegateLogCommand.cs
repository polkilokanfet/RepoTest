using System;
using System.Windows;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Prism.Commands;

namespace HVTApp.UI.Commands
{
    public class DelegateLogCommand : DelegateCommand
    {
        private readonly bool _shutdownAppOnException;

        public DelegateLogCommand(Action executeMethod, bool shutdownAppOnException = true) : base(executeMethod)
        {
            _shutdownAppOnException = shutdownAppOnException;
        }

        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod, bool shutdownAppOnException = true) : base(executeMethod, canExecuteMethod)
        {
            _shutdownAppOnException = shutdownAppOnException;
        }
        
        protected override void Execute(object parameter)
        {
#if DEBUG
            base.Execute(parameter);
#else
            try
            {
                base.Execute(parameter);
            }
            catch (Exception e)
            {
                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
                GlobalAppProperties.MessageService.ShowOkMessageDialog($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.GetAllExceptions());
                if (_shutdownAppOnException)
                {
                    GlobalAppProperties.MessageService.ShowOkMessageDialog($"Информация", "Исключение не обработано. Приложение будет закрыто.");
                    Application.Current.Shutdown();
                }
            }
#endif
        }
    }
}