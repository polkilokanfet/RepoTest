using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.UI.ViewModels;
using HVTApp.Model.Wrapper;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProductsIncludedViewModel : ViewModelBase, IDialogRequestClose
    {
        public ProductIncludedDetailsViewModel ViewModel { get; }
        public DelegateLogCommand OkCommand { get; }
        public bool IsForEach { get; set; } = true;

        public ProductsIncludedViewModel(ProductIncludedWrapper wrapper, IUnitOfWork unitOfWork, IUnityContainer container) : base(container)
        {
            ViewModel = container.Resolve<ProductIncludedDetailsViewModel>();
            ViewModel.Load(wrapper, unitOfWork);
            if (ViewModel.Item.Product == null)
            {
                var productIncludedDefault = GlobalAppProperties.Actual.ProductIncludedDefault;
                if (productIncludedDefault != null)
                {
                    ViewModel.Item.Product = new ProductWrapper(unitOfWork.Repository<Product>().GetById(productIncludedDefault.Id));
                }
            }

            OkCommand = new DelegateLogCommand(
                () =>
                {
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                },
                () =>
                {
                    return ViewModel?.Item.Product != null && ViewModel.Item.Amount > 0;
                });
            ViewModel.Item.PropertyChanged += (s, a) => OkCommand.RaiseCanExecuteChanged();
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}