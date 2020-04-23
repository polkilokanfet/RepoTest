using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using Microsoft.Practices.Unity;
using Prism.Commands;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProductsIncludedViewModel : ViewModelBase, IDialogRequestClose
    {
        public ProductIncludedDetailsViewModel ViewModel { get; }
        public ICommand OkCommand { get; }
        public bool IsForEach { get; set; } = true;

        public ProductsIncludedViewModel(ProductIncludedWrapper wrapper, IUnitOfWork unitOfWork, IUnityContainer container) : base(container)
        {
            ViewModel = container.Resolve<ProductIncludedDetailsViewModel>();
            ViewModel.Load(wrapper, unitOfWork);
            OkCommand = new DelegateCommand(OkCommandExecute, OkCommandCanExecute);
            ViewModel.Item.PropertyChanged += (s, a) => ((DelegateCommand) OkCommand).RaiseCanExecuteChanged();
        }

        private void OkCommandExecute()
        {
            CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
        }

        private bool OkCommandCanExecute()
        {
            return ViewModel?.Item.Product != null && ViewModel.Item.Amount > 0;
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}