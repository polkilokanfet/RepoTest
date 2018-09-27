using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Interfaces.Services;
using HVTApp.Model.Events;
using HVTApp.Model.POCOs;
using HVTApp.Model.Services;
using HVTApp.Services.ProductDesignationService;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Mvvm;

namespace HVTApp.Services.GetProductService
{
    public class GetProductServiceWpf : IGetProductService
    {
        private readonly IUnityContainer _container;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEventAggregator _eventAggregator;
        private Bank _bank;

        public GetProductServiceWpf(IUnityContainer container)
        {
            _container = container;
            _unitOfWork = container.Resolve<IUnitOfWork>();
            _eventAggregator = container.Resolve<IEventAggregator>();
        }

        public async Task LoadAsync()
        {
            var parameters = _unitOfWork.Repository<Parameter>().Find(x => true);
            var products = _unitOfWork.Repository<Product>().Find(x => true);
            var productRelations = await _unitOfWork.Repository<ProductRelation>().GetAllAsync();
            var productBlocks = await _unitOfWork.Repository<ProductBlock>().GetAllAsync();

            _bank = new Bank(products, productBlocks, parameters, productRelations, _container.Resolve<IProductDesignationService>());
        }

        public async Task<Product> GetProductAsync(Product originProduct = null)
        {
            await LoadAsync();

            var selectedProduct = originProduct == null ? null : _bank.Products.Single(x => x.Id == originProduct.Id);

            var productSelector = new ProductSelector(_bank, _bank.Parameters, selectedProduct);
            var window = new SelectProductWindow { DataContext = productSelector, Owner = Application.Current.MainWindow };
            window.ShowDialog();

            //если необходимо создать новый продукт
            if (window.ShoodCreateNew)
            {
                var productNew = await _container.Resolve<INewProductService>().GetNewProductAsync();
                return productNew ?? originProduct;
            }

            //выходим, если пользователь отменил выбор продукта.
            if (!window.DialogResult.HasValue || !window.DialogResult.Value) return originProduct;

            var result = productSelector.SelectedProduct;

            //оставляем только уникальные блоки
            //var blocks = result.GetBlocks().Distinct().ToList();
            //SubstitutionBlocks(result, blocks);

            //загрузка актуальных продуктов
            var products = await _unitOfWork.Repository<Product>().GetAllAsync();
            //если выбранного продукта нет в базе
            if (!products.Contains(result))
            {
                SubstitutionProducts(result, products);
                _unitOfWork.Repository<Product>().Add(result);
                await _unitOfWork.SaveChangesAsync();
                _eventAggregator.GetEvent<AfterSaveProductEvent>().Publish(result);
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