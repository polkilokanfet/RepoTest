using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
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

        public GetProductServiceWpf(IUnityContainer container)
        {
            Container = container;
            UnitOfWork = container.Resolve<IUnitOfWork>();
        }

        /// <summary>
        /// Формирование банка для выбора продукта.
        /// </summary>
        /// <param name="originProduct"></param>
        /// <returns></returns>
        public Bank GetBank(Product originProduct)
        {
            var parameters = UnitOfWork.Repository<Parameter>().GetAll();
            var products = UnitOfWork.Repository<Product>().GetAll();
            var productRelations = UnitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = UnitOfWork.Repository<ProductBlock>().GetAll();

            parameters = ParametersWithoutComplectsParameters(parameters, originProduct);
            parameters = ParametersWithoutNewParameters(parameters, originProduct);

            return new Bank(products, productBlocks, parameters, productRelations);
        }

        /// <summary>
        /// Формирование банка для выбора блока продукта.
        /// </summary>
        /// <param name="requiredParameters">Обязательные параметры в селекторе</param>
        /// <returns></returns>
        public Bank GetBank(IEnumerable<Parameter> requiredParameters)
        {
            var parameters = UnitOfWork.Repository<Parameter>().GetAll();
            var products = UnitOfWork.Repository<Product>().GetAll();
            var productRelations = UnitOfWork.Repository<ProductRelation>().GetAll();
            var productBlocks = UnitOfWork.Repository<ProductBlock>().GetAll();

            parameters = ParametersWithoutComplectsParameters(parameters);
            parameters = ParametersWithoutNewParameters(parameters);

            if (requiredParameters != null)
            {
                //находим максимальное количество пересечений путей параметров
                List<Parameter> requiredPathParameters = null;
                foreach (var requiredParameter in requiredParameters)
                {
                    var pathsParameters = requiredParameter.Paths()
                        .SelectMany(path => path.Parameters)
                        .Distinct()
                        .ToList();

                    requiredPathParameters = requiredPathParameters == null 
                        ? pathsParameters 
                        : pathsParameters.Intersect(requiredPathParameters).ToList();
                }

                //оставляем обязательные параметры "одинокими"
                foreach (var parameter in requiredPathParameters.Union(requiredParameters).Distinct())
                {
                    parameters = parameters.LeaveParameterAsTheOnlyOneInTheGroup(parameter).ToList();
                }
            }

            return new Bank(products, productBlocks, parameters, productRelations);
        }

        /// <summary>
        /// Параметры без параметров "Деталей и Комплектов"
        /// </summary>
        /// <param name="parameters1"></param>
        private List<Parameter> ParametersWithoutComplectsParameters(IEnumerable<Parameter> parameters1)
        {
            var parameters = parameters1.ToList();

            //парметры "обозначение комплекта"
            var complectDesignationParameters = parameters.Where(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectDesignationGroup.Id).ToList();

            //параметры "тип комплекта"
            var complectTypeParameters = parameters.Where(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id).ToList();

            var parametersToExclude = complectTypeParameters.Union(complectDesignationParameters).ToList();

            //параметр "Комплекты и детали"
            var complectsParameter = parameters.SingleOrDefault(parameter => parameter.Id == GlobalAppProperties.Actual.ComplectsParameter.Id);
            if (complectsParameter != null)
            {
                parametersToExclude.Add(complectsParameter);
            }

            return parameters.Except(parametersToExclude).ToList();
        }

        /// <summary>
        /// Параметры без параметров "Деталей и Комплектов"
        /// </summary>
        /// <param name="parameters1"></param>
        /// <param name="selectedProduct"></param>
        private List<Parameter> ParametersWithoutComplectsParameters(IEnumerable<Parameter> parameters1, Product selectedProduct)
        {
            var parameters = parameters1.ToList();

            //парметры "обозначение комплекта"
            var complectDesignationParameters = parameters.Where(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectDesignationGroup.Id).ToList();

            //параметры "тип комплекта"
            var complectTypeParameters = parameters.Where(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id).ToList();

            var parametersToExclude = complectTypeParameters.Union(complectDesignationParameters).ToList();

            //параметр "Комплекты и детали"
            var complectsParameter = parameters.SingleOrDefault(parameter => parameter.Id == GlobalAppProperties.Actual.ComplectsParameter.Id);
            if (complectsParameter != null)
                parametersToExclude.Add(complectsParameter);

            if (selectedProduct != null)
            {
                var ids = selectedProduct.ProductBlock.Parameters.Select(parameter => parameter.Id).ToList();
                parametersToExclude = parametersToExclude.Where(parameter => !ids.Contains(parameter.Id)).ToList();
            }

            return parameters.Except(parametersToExclude).ToList();
        }

        /// <summary>
        /// Параметры без параметров "Новое оборудование"
        /// </summary>
        /// <param name="parameters1"></param>
        private List<Parameter> ParametersWithoutNewParameters(IEnumerable<Parameter> parameters1)
        {
            var parameters = parameters1.ToList();

            //парметры "обозначение"
            var parametersToExclude = parameters.Where(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.NewProductParameterGroup.Id).ToList();

            //параметр "Комплекты и детали"
            var newProductParameter = parameters.SingleOrDefault(parameter => parameter.Id == GlobalAppProperties.Actual.NewProductParameter.Id);
            if (newProductParameter != null)
            {
                parametersToExclude.Add(newProductParameter);
            }

            return parameters.Except(parametersToExclude).ToList();
        }

        /// <summary>
        /// Параметры без параметров "Новое оборудование"
        /// </summary>
        /// <param name="parameters1"></param>
        /// <param name="selectedProduct"></param>
        private List<Parameter> ParametersWithoutNewParameters(IEnumerable<Parameter> parameters1, Product selectedProduct)
        {
            var parameters = parameters1.ToList();

            //парметры "обозначение"
            var parametersToExclude = parameters.Where(parameter => parameter.ParameterGroup.Id == GlobalAppProperties.Actual.NewProductParameterGroup.Id).ToList();

            //параметр "Комплекты и детали"
            var newProductParameter = parameters.SingleOrDefault(parameter => parameter.Id == GlobalAppProperties.Actual.NewProductParameter.Id);
            if (newProductParameter != null)
                parametersToExclude.Add(newProductParameter);

            if (selectedProduct != null)
            {
                var ids = selectedProduct.ProductBlock.Parameters.Select(parameter => parameter.Id).ToList();
                parametersToExclude = parametersToExclude.Where(parameter => !ids.Contains(parameter.Id)).ToList();
            }

            return parameters.Except(parametersToExclude).ToList();
        }

        public Product GetProduct(Product originProduct = null)
        {
            var bank = GetBank(originProduct);

            //предварительно выбранный продукт
            var selectedProduct = originProduct == null 
                ? null 
                : bank.Products.Single(product => product.Id == originProduct.Id);

            var productSelector = new ProductSelector(bank, bank.Parameters, selectedProduct);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();

            //если необходимо создать новый продукт
            if (window.ShoodCreateNew)
            {
                var productNew = Container.Resolve<INewProductService>().GetNewProduct();
                return productNew ?? originProduct;
            }

            //если необходимо выбрать комплект
            if (window.ShoodSelectComplect)
            {
                return Container.Resolve<IGetProductService>().GetComplect(originProduct);
            }

            //выходим, если пользователь отменил выбор продукта.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return originProduct;

            var result = productSelector.SelectedProduct;

            //оставляем только уникальные блоки
            //var blocks = result.GetBlocks().Distinct().ToList();
            //SubstitutionBlocks(result, blocks);

            //загрузка актуальных продуктов
            var products = UnitOfWork.Repository<Product>().GetAll();
            //если выбранного продукта нет в базе
            if (products.Contains(result) == false)
            {
                SubstitutionProducts(result, products);
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(result);
                }
                else
                {
                    throw new Exception("Ошибка при сохранении нового продукта в базу данных.");
                }
            }

            return result;
        }

        public Product GetProduct(IEnumerable<Parameter> requiredParameters)
        {
            var bank = GetBank(requiredParameters.Select(x => UnitOfWork.Repository<Parameter>().GetById(x.Id)));

            //предварительно выбранный продукт
            Product selectedProduct = null;

            var productSelector = new ProductSelector(bank, bank.Parameters);
            var owner = Application.Current.Windows.OfType<Window>().SingleOrDefault(x => x.IsActive);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = owner };
            window.ShowDialog();


            //выходим, если пользователь отменил выбор продукта.
            if (window.DialogResult.HasValue == false || window.DialogResult.Value == false) return null;

            var result = productSelector.SelectedProduct;

            //загрузка актуальных продуктов
            var products = UnitOfWork.Repository<Product>().GetAll();
            //если выбранного продукта нет в базе
            if (products.Contains(result) == false)
            {
                SubstitutionProducts(result, products);
                if (UnitOfWork.SaveEntity(result).OperationCompletedSuccessfully)
                {
                    Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(result);
                }
                else
                {
                    throw new Exception("Ошибка при сохранении нового продукта в базу данных.");
                }
            }

            return result;
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
        /// подмена блоков на уникальные
        /// </summary>
        /// <param name="product">целевой продукт</param>
        /// <param name="uniqBlocks">уникальные блоки</param>
        private void SubstitutionBlocks(Product product, ICollection<ProductBlock> uniqBlocks)
        {
            if (uniqBlocks.Contains(product.ProductBlock))
            {
                product.ProductBlock = uniqBlocks.Single(x => x.Equals(product.ProductBlock));
            }

            foreach (var dependentProduct in product.DependentProducts)
            {
                SubstitutionBlocks(dependentProduct.Product, uniqBlocks);
            }
        }

        private void SubstitutionProducts(Product product, ICollection<Product> uniqProducts)
        {
            foreach (var dependentProduct in product.DependentProducts)
            {
                if (uniqProducts.Contains(dependentProduct.Product))
                {
                    dependentProduct.Product = uniqProducts.Single(product1 => product1.Equals(dependentProduct.Product));
                }

                SubstitutionProducts(dependentProduct.Product, uniqProducts);
            }
        }

        public ProductBlock GetProductBlock(ProductBlock originProductBlock = null, IEnumerable<Parameter> requiredParameters = null)
        {
            var bank = GetBank(requiredParameters?.Select(parameter => UnitOfWork.Repository<Parameter>().GetById(parameter.Id)));
            
            //предварительно выбранный блок продукта
            var selectedProductBlock = originProductBlock == null
                ? null
                : bank.Blocks.Single(block => block.Id == originProductBlock.Id);

            var productBlockSelector = new ProductBlockSelector(bank.Parameters, bank, selectedProductBlock);
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

        public ProductBlock GetProductBlock(IEnumerable<DesignDepartmentParametersAddedBlocks> addedBlocksParameters, ProductBlock originProductBlock = null)
        {
            var banks = addedBlocksParameters
                .Select(x => GetBank(x.Parameters.Select(p => UnitOfWork.Repository<Parameter>().GetById(p.Id))))
                .ToList();
            var bankParameters = banks.SelectMany(x => x.Parameters).Distinct().ToList();
            var firstBank = banks.First();
            var bank = new Bank(firstBank.Products, firstBank.Blocks, bankParameters, firstBank.Relations);

            //предварительно выбранный блок продукта
            ProductBlock selectedProductBlock = originProductBlock == null
                ? null
                : bank.Blocks.Single(block => block.Id == originProductBlock.Id);

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