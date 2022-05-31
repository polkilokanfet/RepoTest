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
        private readonly Action _executeMethod;
        private readonly Func<bool> _canExecuteMethod;
        private readonly bool _shutdownAppOnException = true;

        public DelegateLogCommand()
        {
        }

        public DelegateLogCommand(Action executeMethod, bool shutdownAppOnException = true)
        {
            _executeMethod = executeMethod ?? throw new ArgumentNullException(nameof(executeMethod));
            _shutdownAppOnException = shutdownAppOnException;
        }

        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod, bool shutdownAppOnException = true) : this(executeMethod, shutdownAppOnException)
        {
            _canExecuteMethod = canExecuteMethod ?? throw new ArgumentNullException(nameof(canExecuteMethod));
        }


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
            ExecuteMethod(parameter);
#else
            try
            {
                ExecuteMethod(parameter);
            }
            catch (Exception e)
            {
                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
                GlobalAppProperties.MessageService.ShowOkMessageDialog($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.PrintAllExceptions());
                if (_shutdownAppOnException)
                {
                    if (GlobalAppProperties.EventServiceClient != null)
                        GlobalAppProperties.EventServiceClient.Stop();
                    Application.Current.Shutdown();
                }
            }
#endif
        }

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
        }
    }

    //    public class DelegateLogCommand : DelegateCommand
    //    {
    //        private readonly bool _shutdownAppOnException;

    //        public DelegateLogCommand(Action executeMethod, bool shutdownAppOnException = true) : base(executeMethod)
    //        {
    //            _shutdownAppOnException = shutdownAppOnException;
    //        }

    //        public DelegateLogCommand(Action executeMethod, Func<bool> canExecuteMethod, bool shutdownAppOnException = true) : base(executeMethod, canExecuteMethod)
    //        {
    //            _shutdownAppOnException = shutdownAppOnException;
    //        }

    //        public new void Execute()
    //        {
    //#if DEBUG
    //            base.Execute();
    //#else
    //            try
    //            {
    //                base.Execute();
    //            }
    //            catch (Exception e)
    //            {
    //                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
    //                GlobalAppProperties.MessageService.ShowOkMessageDialog($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.PrintAllExceptions());
    //                if (_shutdownAppOnException)
    //                {
    //                    GlobalAppProperties.EventServiceClient.Stop();
    //                    Application.Current.Shutdown();
    //                }
    //            }
    //#endif
    //        }

    //        protected override void Execute(object parameter)
    //        {
    //#if DEBUG
    //            base.Execute(parameter);
    //#else
    //            try
    //            {
    //                base.Execute(parameter);
    //            }
    //            catch (Exception e)
    //            {
    //                GlobalAppProperties.HvtAppLogger.LogError(e.GetType().Name, e);
    //                GlobalAppProperties.MessageService.ShowOkMessageDialog($"Исключение в DelegateLogCommand: {e.GetType().Name}", e.PrintAllExceptions());
    //                if (_shutdownAppOnException)
    //                {
    //                    GlobalAppProperties.EventServiceClient.Stop();
    //                    Application.Current.Shutdown();
    //                }
    //            }
    //#endif
    //        }
    //    }
}