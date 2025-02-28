using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services.DialogService;
using HVTApp.Model;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.UI.Commands;
using HVTApp.UI.ViewModels;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public class ProductIncludedWrapper1 : WrapperBase<ProductIncluded>
    {
        public ProductIncludedWrapper1(ProductIncluded model) : base(model) { }

        /// <summary>
        /// Количество
        /// </summary>
        public int Amount
        {
            get => Model.Amount;
            set => SetValue(value);
        }
        public int AmountOriginalValue => GetOriginalValue<int>(nameof(Amount));
        public bool AmountIsChanged => GetIsChanged(nameof(Amount));
    }


    public class ProductIncludedViewModel : ViewModelBase, IDialogRequestClose
    {
        public ProductIncludedWrapper1 Item { get; }

        public DelegateLogCommand SelectProductCommand { get; }
        public DelegateLogCommand OkCommand { get; }

        public bool IsForEach { get; set; } = true;

        public ProductIncludedViewModel(IUnitOfWork unitOfWork, IUnityContainer container) : base(container)
        {
            var productIncludedDefault = unitOfWork.Repository<Product>().GetById(GlobalAppProperties.Actual.ProductIncludedDefault.Id);
            Item = new ProductIncludedWrapper1(new ProductIncluded { Product = productIncludedDefault });

            SelectProductCommand = new DelegateLogCommand(
                () =>
                {
                    var product = Container.Resolve<IGetProductService>().GetProduct(Item.Model.Product);
                    if (product != null)
                    {
                        product = UnitOfWork.Repository<Product>().GetById(product.Id);
                        Item.Model.Product = product;
                    }
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