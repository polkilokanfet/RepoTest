using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.DataAccess;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.GetProductService.Complects;
using Microsoft.Practices.Unity;
using Prism.Events;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private IUnityContainer Container { get; }
        private IUnitOfWork UnitOfWork { get; }

        private readonly BankFactory _bankFactory;

        public GetProductServiceWpf(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = container.Resolve<IUnitOfWork>();
            _bankFactory = new BankFactory(UnitOfWork);
        }

        public Product GetProduct(Product originProduct = null)
        {
            return this.GetProduct(_bankFactory.CreateBank(originProduct), originProduct);
        }

        public Product GetProduct(IEnumerable<Parameter> requiredParameters)
        {
            return this.GetProduct(_bankFactory.CreateBank(requiredParameters.ChangeUnitOfWork(UnitOfWork)));
        }

        private Product GetProduct(Bank bank, Product originProduct = null)
        {
            //предварительно выбранный продукт
            var selectedProduct = originProduct?.ChangeUnitOfWork(UnitOfWork);

            var productSelector = new ProductSelector(bank, bank.Parameters, selectedProduct);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();

            //если необходимо выбрать комплект
            if (window.ShouldSelectComplect)
            {
                return Container.Resolve<IGetProductService>().GetComplect(originProduct);
            }

            //выходим, если пользователь отменил выбор продукта.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProduct;

            var result = productSelector.SelectedProduct;
            productSelector.Dispose();

            //если выбранного продукта нет в базе
            if (((IProductRepository)UnitOfWork.Repository<Product>()).CanAdd(result).OperationCompletedSuccessfully)
            {
                SaveProduct(result);
            }
            else
            {
                var result1 = result;
                result = UnitOfWork.Repository<Product>().FindAsNoTracking(x => x.Equals(result1)).Single();
            }

            return result;
        }

        public Product GetProduct(IUnitOfWork unitOfWork, Product product)
        {
            //загрузка актуальных продуктов
            var products = unitOfWork.Repository<Product>().GetAll();

            var result = products.SingleOrDefault(x => x.Equals(product));
            if (result != null) return result;

            //если выбранного продукта нет в базе, сохраняем его
            result = product;
            this.SaveProduct(result);

            return result;
        }


        private void SaveProduct(Product product)
        {
            var products = UnitOfWork.Repository<Product>().GetAll();
            var blocks = UnitOfWork.Repository<ProductBlock>().GetAll();

            SubstitutionBlocksAndProducts(product, products, blocks);

            var operationResult = UnitOfWork.SaveEntity(product);

            if (operationResult.OperationCompletedSuccessfully == false)
                throw new Exception("Ошибка при сохранении нового продукта в базу данных.", operationResult.Exception);

            Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(product);
        }

        public Product GetComplect(Product originProduct = null)
        {
            var complectViewModel = Container.Resolve<ComplectsViewModel>();
            complectViewModel.ShowDialog();

            return complectViewModel.IsSelected
                ? complectViewModel.SelectedItem.Product
                : originProduct;
        }


        /// <summary>
        /// Замена новых блоков и продуктов на сохранённые
        /// </summary>
        /// <param name="product"></param>
        /// <param name="savedProducts">Сохраненные продукты</param>
        /// <param name="savedBlocks"></param>
        private void SubstitutionBlocksAndProducts(Product product, ICollection<Product> savedProducts, ICollection<ProductBlock> savedBlocks)
        {
            //замена блоков на сохранённые
            var block = savedBlocks.SingleOrDefault(productBlock => product.ProductBlock.Equals(productBlock));
            if (block != null)
            {
                product.ProductBlock = block;
            }
            else
            {
                savedBlocks.Add(product.ProductBlock);
            }

            //для каждого зависиммого продукта
            foreach (var dependentProduct in product.DependentProducts)
            {
                var savedProduct = savedProducts.SingleOrDefault(product1 => product1.Equals(dependentProduct.Product));
                //если продукт есть в сохраненных, меняем его
                if (savedProduct != null)
                {
                    dependentProduct.Product = savedProduct;
                }
                else
                {
                    savedProducts.Add(dependentProduct.Product);
                }

                SubstitutionBlocksAndProducts(dependentProduct.Product, savedProducts, savedBlocks);
            }
        }

        public ProductBlock GetProductBlock(ProductBlock originProductBlock = null, IEnumerable<Parameter> requiredParameters = null)
        {
            var bank = _bankFactory.CreateBank(requiredParameters?.ChangeUnitOfWork(UnitOfWork));
            
            //предварительно выбранный блок продукта
            var selectedProductBlock = originProductBlock?.ChangeUnitOfWork(UnitOfWork);

            var productBlockSelector = new ProductBlockSelector(bank.Parameters, bank, selectedProductBlock);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductBlockWindow { DataContext = productBlockSelector, Owner = owner };
            window.ShowDialog();

            //выходим, если пользователь отменил выбор блока продукта.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProductBlock;

            var result = productBlockSelector.SelectedBlock;

            //загрузка актуальных блоков продуктов
            var productBlocks = UnitOfWork.Repository<ProductBlock>().GetAll();
            //если выбранного блока продукта нет в базе
            if (productBlocks.Contains(result) == false)
            {
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductBlockEvent>().Publish(result);
                }
                else
                {
                    throw new Exception("Ошибка при сохранении нового блока продукта в базу данных.");
                }
            }

            return result;
        }

        public ProductBlock GetProductBlock(IEnumerable<IParametersContainer> parametersContainers, ProductBlock originProductBlock = null)
        {
            var parameterContainers = parametersContainers as IParametersContainer[] ?? parametersContainers.ToArray();
            var banks = parameterContainers
                .Select(x => _bankFactory.CreateBank(x.Parameters.ChangeUnitOfWork(UnitOfWork)))
                .ToList();

            //обязательные параметры в группах
            var requiredParameters = parameterContainers
                .SelectMany(x => x.Parameters)
                .Distinct()
                .ToList();

            //удаляем из групп обязательных параметров всё, кроме обязательных параметров
            var bankParameters = banks
                .SelectMany(x => x.Parameters)
                .Distinct()
                .LeaveParametersAloneInGroup(requiredParameters)
                .ToList();

            var bank = _bankFactory.CreateBank(bankParameters);

            //предварительно выбранный блок продукта
            var selectedProductBlock = originProductBlock?.ChangeUnitOfWork(UnitOfWork);

            var productBlockSelector = new ProductBlockSelector(bank.Parameters, bank, selectedProductBlock);
            var originParameterSelector = productBlockSelector.ParameterSelectors.FirstOrDefault(x => x.ParametersFlaged.Any(p => p.Parameter.IsOrigin));
            if (originParameterSelector != null)
            {
                originParameterSelector.SelectedParameterFlaged = originParameterSelector.ParametersFlaged.First();
            }

            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductBlockWindow() { DataContext = productBlockSelector, Owner = owner };
            window.ShowDialog();

            //выходим, если пользователь отменил выбор блока продукта.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProductBlock;

            var result = productBlockSelector.SelectedBlock;

            //загрузка актуальных блоков продуктов
            var productBlocks = UnitOfWork.Repository<ProductBlock>().GetAll();
            //если выбранного блока продукта нет в базе
            if (productBlocks.Contains(result) == false)
            {
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductBlockEvent>().Publish(result);
                }
                else
                {
                    throw new Exception("Ошибка при сохранении нового блока продукта в базу данных.");
                }
            }

            return result;
        }
    }

}