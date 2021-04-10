using System.Collections.Generic;
using System.Linq;
using System.Windows;
using HVTApp.Infrastructure;
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
        /// Параметры без параметров "Деталей и Комплектов"
        /// </summary>
        /// <param name="parameters1"></param>
        /// <param name="selectedProduct"></param>
        private List<Parameter> ParametersWithoutComplectsParameters(IEnumerable<Parameter> parameters1, Product selectedProduct)
        {
            var parameters = parameters1.ToList();

            //парметры "обозначение комплекта"
            var complectDesignationParameters = parameters.Where(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectDesignationGroup.Id).ToList();

            //параметры "тип комплекта"
            var complectTypeParameters = parameters.Where(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.ComplectsGroup.Id).ToList();

            var parametersToExclude = complectTypeParameters.Union(complectDesignationParameters).ToList();

            //параметр "Комплекты и детали"
            var complectsParameter = parameters.SingleOrDefault(x => x.Id == GlobalAppProperties.Actual.ComplectsParameter.Id);
            if (complectsParameter != null)
                parametersToExclude.Add(complectsParameter);

            if (selectedProduct != null)
            {
                var ids = selectedProduct.ProductBlock.Parameters.Select(x => x.Id).ToList();
                parametersToExclude = parametersToExclude.Where(x => !ids.Contains(x.Id)).ToList();
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
            var parametersToExclude = parameters.Where(x => x.ParameterGroup.Id == GlobalAppProperties.Actual.NewProductParameterGroup.Id).ToList();

            //параметр "Комплекты и детали"
            var newProductParameter = parameters.SingleOrDefault(x => x.Id == GlobalAppProperties.Actual.NewProductParameter.Id);
            if (newProductParameter != null)
                parametersToExclude.Add(newProductParameter);

            if (selectedProduct != null)
            {
                var ids = selectedProduct.ProductBlock.Parameters.Select(x => x.Id).ToList();
                parametersToExclude = parametersToExclude.Where(x => !ids.Contains(x.Id)).ToList();
            }

            return parameters.Except(parametersToExclude).ToList();
        }

        public Product GetProduct(Product originProduct = null)
        {
            var bank = GetBank(originProduct);

            //предварительно выбранный продукт
            var selectedProduct = originProduct == null 
                ? null 
                : bank.Products.Single(x => x.Id == originProduct.Id);

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
                var complectViewModel = Container.Resolve<ComplectsViewModel>();
                complectViewModel.ShowDialog();
                return complectViewModel.IsSelected 
                    ? complectViewModel.SelectedItem.Product 
                    : originProduct;
            }

            //выходим, если пользователь отменил выбор продукта.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;

            //оставляем только уникальные блоки
            //var blocks = result.GetBlocks().Distinct().ToList();
            //SubstitutionBlocks(result, blocks);

            //загрузка актуальных продуктов
            var products = UnitOfWork.Repository<Product>().GetAll();
            //если выбранного продукта нет в базе
            if (!products.Contains(result))
            {
                SubstitutionProducts(result, products);
                UnitOfWork.Repository<Product>().Add(result);
                UnitOfWork.SaveChanges();
                Container.Resolve<IEventAggregator>().GetEvent<AfterSaveProductEvent>().Publish(result);
            }

            return result;
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
                    dependentProduct.Product = uniqProducts.Single(x => x.Equals(dependentProduct.Product));
                }

                SubstitutionProducts(dependentProduct.Product, uniqProducts);
            }
        }
    }
}