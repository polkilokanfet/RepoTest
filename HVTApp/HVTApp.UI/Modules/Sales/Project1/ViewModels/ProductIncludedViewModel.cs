using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;
using Prism.Mvvm;

namespace HVTApp.UI.Modules.Sales.Project1.ViewModels
{
    public class ProductIncludedViewModel : BindableBase, IDialogRequestClose
    {
        public ProductIncludedWrapper1 Item { get; }

        public bool IsForEach { get; set; } = true;

        public DelegateLogCommand SelectProductCommand { get; }
        public DelegateLogCommand OkCommand { get; }

        public ProductIncludedViewModel(IGetProductService getProductService, IUnitOfWork unitOfWork)
        {
            var productDefault = unitOfWork.Repository<Product>().GetById(GlobalAppProperties.Actual.ProductIncludedDefault.Id);
            Item = new ProductIncludedWrapper1(new ProductIncluded { Product = productDefault });

            SelectProductCommand = new DelegateLogCommand(
                () =>
                {
                    var product = getProductService.GetProduct(Item.Model.Product);
                    if (product == null) return;
                    product = unitOfWork.Repository<Product>().GetById(product.Id);
                    Item.Model.Product = product;
                    RaisePropertyChanged(nameof(Item));
                });


            OkCommand = new DelegateLogCommand(
                () =>
                {
                    CloseRequested?.Invoke(this, new DialogRequestCloseEventArgs(true));
                });
        }

        public event EventHandler<DialogRequestCloseEventArgs> CloseRequested;
    }
}