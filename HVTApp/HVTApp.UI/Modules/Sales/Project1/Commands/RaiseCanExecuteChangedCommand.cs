using System;
using System.Windows.Input;

namespace HVTApp.UI.Modules.Sales.Project1.Commands
{
    public abstract class RaiseCanExecuteChangedCommand : ICommand
    {
        private bool _canExecuteFlag;

        public void RaiseCanExecuteChanged()
        {
            var canExecute = CanExecute(null);
            if (_canExecuteFlag == canExecute) return;
            _canExecuteFlag = canExecute;
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        public abstract bool CanExecute(object parameter);

        public virtual void Execute(object parameter)
        {
            RaiseCanExecuteChanged();
        }

        public event EventHandler CanExecuteChanged;
    }
}