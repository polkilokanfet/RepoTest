using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.UI.Commands;
using Microsoft.Practices.Unity;

namespace HVTApp.UI.Modules.Products.ViewModels
{
    public class ProductReplacementViewModel : ViewModelBase
    {
        private Product _productReplaceable;
        private Product _productTarget;

        /// <summary>
        /// Заменяемый продукт
        /// </summary>
        public Product ProductReplaceable
        {
            get => _productReplaceable;
            set
            {
                _productReplaceable = value;
                ReplaceCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        /// <summary>
        /// Продукт на который будет заменён заменяемый продукт
        /// </summary>
        public Product ProductTarget
        {
            get { return _productTarget; }
            set
            {
                _productTarget = value;
                ReplaceCommand.RaiseCanExecuteChanged();
                RaisePropertyChanged();
            }
        }

        public DelegateLogCommand ProductReplaceableCommand { get; }
        public DelegateLogCommand ProductTargetCommand { get; }
        public DelegateLogCommand ReplaceCommand { get; }

        public ProductReplacementViewModel(IUnityContainer container) : base(container)
        {
            ProductReplaceableCommand = new DelegateLogCommand(
                () =>
                {
                    var products = UnitOfWork.Repository<Product>().GetAll();
                    var product = Container.Resolve<ISelectService>().SelectItem(products);
                    if (product != null)
                    {
                        ProductReplaceable = UnitOfWork.Repository<Product>().GetById(product.Id);
                    }
                });

            ProductTargetCommand = new DelegateLogCommand(
                () =>
                {
                    var product = Container.Resolve<IGetProductService>().GetProduct(ProductTarget);
                    if (product != null)
                    {
                        ProductTarget = UnitOfWork.Repository<Product>().GetById(product.Id);
                    }
                });

            ReplaceCommand = new DelegateLogCommand(
                () =>
                {
                    var salesUnits = UnitOfWork.Repository<SalesUnit>().Find(x => x.Product.Id == ProductReplaceable.Id);
                    salesUnits.ForEach(x => x.Product = ProductTarget);

                    var offerUnits = UnitOfWork.Repository<OfferUnit>().Find(x => x.Product.Id == ProductReplaceable.Id);
                    offerUnits.ForEach(x => x.Product = ProductTarget);

                    try
                    {
                        UnitOfWork.Repository<Product>().Delete(ProductReplaceable);

                        UnitOfWork.SaveChanges();
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Заменено", $"SalesUnits: {salesUnits.Count}\nOfferUnits: {offerUnits.Count}\n\nЗамененный продукт удален!");
                    }
                    catch (Exception e)
                    {
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", e.PrintAllExceptions());
                    }
                },
                () => ProductReplaceable != null && ProductTarget != null);
        }
    }
}