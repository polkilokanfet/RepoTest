using System;
using System.Windows.Input;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Infrastructure.Interfaces.Services.SelectService;
using HVTApp.Infrastructure.Services;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using Microsoft.Practices.Unity;
using Prism.Commands;

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
            get { return _productReplaceable; }
            set
            {
                _productReplaceable = value;
                ((DelegateCommand)ReplaceCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
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
                ((DelegateCommand)ReplaceCommand).RaiseCanExecuteChanged();
                OnPropertyChanged();
            }
        }

        public ICommand ProductReplaceableCommand { get; }
        public ICommand ProductTargetCommand { get; }
        public ICommand ReplaceCommand { get; }

        public ProductReplacementViewModel(IUnityContainer container) : base(container)
        {
            ProductReplaceableCommand = new DelegateCommand(
                () =>
                {
                    var products = UnitOfWork.Repository<Product>().GetAll();
                    var product = Container.Resolve<ISelectService>().SelectItem(products);
                    if (product != null)
                    {
                        ProductReplaceable = UnitOfWork.Repository<Product>().GetById(product.Id);
                    }
                });

            ProductTargetCommand = new DelegateCommand(
                () =>
                {
                    var product = Container.Resolve<IGetProductService>().GetProduct(ProductTarget);
                    if (product != null)
                    {
                        ProductTarget = UnitOfWork.Repository<Product>().GetById(product.Id);
                    }
                });

            ReplaceCommand = new DelegateCommand(
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
                        Container.Resolve<IMessageService>().ShowOkMessageDialog("Exception", e.GetAllExceptions());
                    }
                },
                () => ProductReplaceable != null && ProductTarget != null);
        }
    }
}