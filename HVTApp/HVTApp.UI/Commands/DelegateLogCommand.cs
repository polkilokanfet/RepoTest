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

        public DelegateLogCommand(Action executeMethod)
        {
            _delegateCommand = new DelegateCommand(executeMethod, () => true);
        }

        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _delegateCommand = new DelegateCommand(executeMethod, canExecuteMethod);
        }

        public bool CanExecute(object parameter)
        {
            return DelegateCommand.CanExecute();
        }

        public void Execute(object parameter)
        {
            try
            {
                DelegateCommand.Execute();
            }
            catch (Exception e)
            {
                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
                GlobalAppProperties.MessageService.ShowOkMessageDialog($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.GetAllExceptions());
                Application.Current.Shutdown();
            }
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            DelegateCommand.RaiseCanExecuteChanged();
        }
    }
}