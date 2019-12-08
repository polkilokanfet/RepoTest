using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public class SalesUnitsViewModel : BindableBase, IDialogRequestClose
    {
        private int _amount = 1;

        public int Amount
        {
            get { return _amount; }
            set
            {
                if (Equals(_amount, value)) return;
                if (value <= 0) return;
                _amount = value;
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public SalesUnitDetailsViewModel ViewModel { get; }

        public ICommand OkCommand { get; }

        public SalesUnitsViewModel(SalesUnitWrapper item, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            ViewModel = container.Resolve<SalesUnitDetailsViewModel>();
            ViewModel.Load(item, unitOfWork);

            OkCommand = new DelegateCommand(
                () => { CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true)); }, 
                () => ViewModel.Item.IsValid);
            ViewModel.Item.PropertyChanged += (sender, args) => ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}