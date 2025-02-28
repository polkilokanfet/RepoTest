using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using HVTApp.UI.Modules.Sales.Project1.Wrappers;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProductIncludedViewModel : IDialogRequestClose
    {
        public ProductIncludedWrapper1 Item { get; }

        public bool IsForEach { get; set; } = true;

        public DelegateLogCommand SelectProductCommand { get; }
        public DelegateLogCommand OkCommand { get; }

        public ProductIncludedViewModel(IUnitOfWork unitOfWork, IGetProductService getProductService)
        {
            var productIncludedDefault = unitOfWork.Repository<Product>().GetById(GlobalAppProperties.Actual.ProductIncludedDefault.Id);
            Item = new ProductIncludedWrapper1(new ProductIncluded { Product = productIncludedDefault });

            SelectProductCommand = new DelegateLogCommand(
                () =>
                {
                    var product = getProductService.GetProduct(Item.Model.Product);
                    if (product == null) return;
                    product = unitOfWork.Repository<Product>().GetById(product.Id);
                    Item.Model.Product = product;
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