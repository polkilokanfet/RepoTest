using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model.Services;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.ViewModels.Groups
{
    public class SalesUnitsViewModel : BindableBase, IDialogRequestClose
    {
        private int _amount = 1;
        public int Amount
        {
            get => _amount;
            set
            {
                if (Equals(_amount, value)) return;
                if (value <= 0) return;
                _amount = value;
                OkCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public SalesUnitDetailsViewModel ViewModel { get; }

        public DelegateLogCommand OkCommand { get; }

        public SalesUnitsViewModel(SalesUnitWrapper item, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            ViewModel = container.Resolve<SalesUnitDetailsViewModel>();
            ViewModel.Load(item, unitOfWork);

            OkCommand = new DelegateLogCommand(
                () => { CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true)); }, 
                () => ViewModel.Item.IsValid);
            ViewModel.Item.PropertyChanged += (sender, args) => (OkCommand).RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}