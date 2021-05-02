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
        private readonly DelegateCommand _delegateCommand;

        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod)
        {
            _delegateCommand = new DelegateCommand(executeMethod, canExecuteMethod);
            _delegateCommand.CanExecuteChanged += (sender, args) =>
            {
                CanExecuteChanged?.Invoke(sender, args);
            };
        }

        public bool CanExecute(object parameter)
        {
            return _delegateCommand.CanExecute();
        }

        public void Execute(object parameter)
        {
            try
            {
                _delegateCommand.Execute();
            }
            catch (Exception e)
            {
                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
                GlobalAppProperties.MessageService.ShowOkMessageDialog($"DelegateLogCommand: {e.GetType().Name}", e.GetAllExceptions());
                Application.Current.Shutdown();
            }
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            _delegateCommand.RaiseCanExecuteChanged();
        }
    }
}