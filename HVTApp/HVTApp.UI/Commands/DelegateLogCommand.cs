using System;
using System.Windows;
using System.Windows.Input;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using Prism.Commands;

namespace HVTApp.UI.Commands
{
    public class DelegateLogCommand : ICommand
    {
        private readonly bool _shutdownAppOnException;
        private DelegateCommand _delegateCommand;

        private DelegateCommand DelegateCommand
        {
            get => _delegateCommand;
            set
            {
                _delegateCommand = value;
                _delegateCommand.CanExecuteChanged += (sender, args) =>
                {
                    CanExecuteChanged?.Invoke(sender, args);
                };
            }
        }

        public DelegateLogCommand(Action executeMethod, bool shutdownAppOnException = true)
        {
            _delegateCommand = new DelegateCommand(executeMethod, () => true);
            _shutdownAppOnException = shutdownAppOnException;
        }

        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod, bool shutdownAppOnException = true)
        {
            _delegateCommand = new DelegateCommand(executeMethod, canExecuteMethod);
            _shutdownAppOnException = shutdownAppOnException;
        }

        public bool CanExecute(object parameter = null)
        {
            return DelegateCommand.CanExecute();
        }

        public void Execute(object parameter = null)
        {
#if DEBUG
            DelegateCommand.Execute();
#else
            try
            {
                DelegateCommand.Execute();
            }
            catch (Exception e)
            {
                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
                GlobalAppProperties.MessageService.ShowOkMessageDialog($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.GetAllExceptions());
                if (_shutdownAppOnException)
                {
                    Application.Current.Shutdown();
                }
            }
#endif
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            DelegateCommand.RaiseCanExecuteChanged();
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}