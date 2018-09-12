using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Events;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class SalesUnitsViewModel : BindableBase, IDialogRequestClose
    {
        private int _amount = 1;

        public SalesUnitDetailsViewModel ViewModel { get; } 

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (Equals(_amount, value)) return;
                _amount = value;
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ICommand OkCommand { get; }

        public SalesUnitsViewModel(SalesUnitWrapper item, IUnityContainer container, IWrapperDataService wrapperDataService)
        {
            ViewModel = container.Resolve<SalesUnitDetailsViewModel>();
            ViewModel.Load(item, wrapperDataService);
            Action okCommandExecute = () => CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            Func<bool> okCommandCanExecute = () => Amount > 0 && ViewModel.Item.IsValid;
            OkCommand = new DelegateCommand(okCommandExecute, okCommandCanExecute);
            ViewModel.Item.PropertyChanged += (sender, args) => ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}