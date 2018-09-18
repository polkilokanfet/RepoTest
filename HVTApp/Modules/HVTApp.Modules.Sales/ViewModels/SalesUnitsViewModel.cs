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
                if (value <= 0) return;
                _amount = value;
                ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ICommand OkCommand { get; }

        public SalesUnitsViewModel(SalesUnitWrapper item, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            ViewModel = container.Resolve<SalesUnitDetailsViewModel>();
            ViewModel.Load(item, unitOfWork);

            OkCommand = new DelegateCommand(OkCommandExecute, OkCommandCanExecute);
            ViewModel.Item.PropertyChanged += (sender, args) => ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        private bool OkCommandCanExecute()
        {
            return ViewModel.Item.IsValid;
        }

        private void OkCommandExecute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}