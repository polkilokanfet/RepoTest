using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.ViewModels;
using HVTApp.UI.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;
using Prism.Mvvm;

namespace HVTApp.Modules.Sales.ViewModels
{
    public class OfferUnitsViewModel : BindableBase, IDialogRequestClose
    {
        private int _amount = 1;

        public OfferUnitDetailsViewModel ViewModel { get; } 

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

        public OfferUnitsViewModel(OfferUnitWrapper item, IUnityContainer container, IUnitOfWork unitOfWork)
        {
            ViewModel = container.Resolve<OfferUnitDetailsViewModel>();
            ViewModel.Load(item, unitOfWork);
            Action okCommandExecute = () => CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
            Func<bool> okCommandCanExecute = () => Amount > 0 && ViewModel.Item.IsValid;
            OkCommand = new DelegateCommand(okCommandExecute, okCommandCanExecute);
            ViewModel.Item.PropertyChanged += (sender, args) => ((DelegateCommand)OkCommand).RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}