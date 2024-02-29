using System;
using System.Windows;
using HVTApp.Infrastructure.Extensions;
using HVTApp.Infrastructure.Interfaces;
using HVTApp.Model;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Commands
{
    public class DelegateLogCommand : BindableBase, ICommandIsVisibleWhenCanExecute
    {
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        private readonly bool _shutdownAppOnException = true;

        #region ctors

        public DelegateLogCommand()
        {
        }

        public DelegateLogCommand(Action executeMethod, bool shutdownAppOnException = true)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
            _shutdownAppOnException = shutdownAppOnException;
        }

        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod, bool shutdownAppOnException = true) 
            : this(executeMethod, shutdownAppOnException)
        {
            _canExecuteMethod = canExecuteMethod ?? throw new ArgumentNullException(nameof(canExecuteMethod));
        }

        #endregion

        #region Execute

        protected virtual void ExecuteMethod()
        {
            _executeMethod?.Invoke();
        }


        protected virtual void ExecuteMethod(object parameter)
        {
            this.ExecuteMethod();
        }


        public void Execute()
        {
            this.Execute(null);
        }

        public void Execute(object parameter)
        {
#if DEBUG
            this.ExecuteMethod(parameter);
#else
            try
            {
                ExecuteMethod(parameter);
            }
            catch (Exception e)
            {
                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
                GlobalAppProperties.MessageService.Message($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.PrintAllExceptions());
                if (_shutdownAppOnException)
                {
                    Application.Current.Shutdown();
                }
            }
#endif
        }

        #endregion

        #region CanExecute

        public event EventHandler CanExecuteChanged;

        protected virtual bool CanExecuteMethod()
        {
            return _canExecuteMethod?.Invoke() ?? true;
        }

        public bool CanExecute()
        {
            return this.CanExecute(null);
        }

        public bool CanExecute(object parameter)
        {
            return CanExecuteMethod();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
            RaisePropertyChanged(nameof(IsVisible));
        }

        #endregion

        public bool IsVisible => CanExecute();
    }
}