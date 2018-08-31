using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Services.ProductDesignationService;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly Bank _bank;
        private readonly IProductDesignationService _designator;

        public ProductBlockSelector ProductBlockSelector { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; } = new ObservableCollection<ProductSelector>();
        public int Amount { get; }

        public Product SelectedProduct
        {
            get
            {
                var product = new Product
                {
                    ProductBlock = ProductBlockSelector.SelectedProductBlock,
                    DependentProducts = ProductSelectors.Select(x => x.SelectedProduct).ToList()
                };
                product.Designation = _designator.GetDesignation(product);
                product.ProductType = _designator.GetProductType(product);

                var existsProduct = _bank.Products.SingleOrDefault(x => x.Equals(product));

                return existsProduct ?? product;
            }
        }

        public ProductSelector(Bank bank, IProductDesignationService designator, IEnumerable<Parameter> parameters, 
                               Product selectedProduct = null, int amount = 1)
        {
            if (bank == null) throw new ArgumentNullException(nameof(bank));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            _bank = bank;
            _designator = designator;

            Amount = amount;
            ProductBlockSelector = new ProductBlockSelector(parameters, _bank.ProductBlocks, selectedProduct?.ProductBlock);
            ProductBlockSelector.SelectedProductBlockChanged += bs =>
            {
                RefreshProductSelectors();
                SelectedProductChanged?.Invoke();
                OnPropertyChanged(nameof(SelectedProduct));
            };

            if (selectedProduct == null)
            {
                //выбираем первый параметр базового селектора
                ProductBlockSelector.SelectFirstParameter();
            }
            else
            {
                foreach (var kvp in GetDictionaryOfMatching(selectedProduct))
                {
                    //редактируем список параметров
                    var usefullParameters = bank.Parameters.GetUsefull(kvp.Key.ChildProductParameters);
                    var productSelector = new ProductSelector(bank, _designator, usefullParameters, kvp.Value);
                    ProductSelectors.Add(productSelector);
                    productSelector.SelectedProductChanged += ProductSelectorOnSelectedProductChanged;
                }
            }
        }

        private List<ProductRelation> GetActualProductRelations(IEnumerable<Parameter> forParameters = null)
        {
            var parameters = forParameters ?? ProductBlockSelector.SelectedProductBlock.Parameters;
            return _bank.ProductRelations.Where(x => x.ParentProductParameters.AllContainsIn(parameters)).ToList();
        }

        private void RefreshProductSelectors()
        {
            //загружаем актуальные связи продуктов, упорядоченные по количеству параметров, зависимого продукта
            var actualProductRelations = GetActualProductRelations().OrderBy(x => x.ChildProductParameters.Count).ToList();
            //упорядочиваем селектры продуктов по уменьшению количества параметров
            var productSelectors = new List<ProductSelector>(ProductSelectors.
                                   OrderByDescending(x => x.SelectedProduct.ProductBlock.Parameters.Count));

            var relaitionsDictionary = new Dictionary<ProductRelation, int>();
            foreach (var actualProductRelation in actualProductRelations)
            {
                relaitionsDictionary.Add(actualProductRelation, actualProductRelation.ChildProductsAmount);
            }

            //удаление неактуальных селекторов и чистка связей
            foreach (var productSelector in productSelectors)
            {
                var relation = actualProductRelations.FirstOrDefault(x => x.ChildProductParameters.AllContainsIn(
                               productSelector.ProductBlockSelector.SelectedProductBlock.Parameters));

                if (relation == null)
                {
                    ProductSelectors.Remove(productSelector);
                    productSelector.SelectedProductChanged -= ProductSelectorOnSelectedProductChanged;
                }
                else
                {
                    relaitionsDictionary[relation] -= productSelector.Amount;
                    if (relaitionsDictionary[relation] == 0)
                    {
                        actualProductRelations.Remove(relation);
                    }
                }
            }

            //добавление новых актуальных селекторов
            foreach (var productRelation in actualProductRelations)
            {
                for (int i = 0; i < relaitionsDictionary[productRelation]; i++)
                {
                    var productSelector = new ProductSelector(_bank, _designator,
                        _bank.Parameters.GetUsefull(productRelation.ChildProductParameters));
                    ProductSelectors.Add(productSelector);
                    productSelector.SelectedProductChanged += ProductSelectorOnSelectedProductChanged;
                }
            }
        }

        //реакция на изменение дочернего продукта
        private void ProductSelectorOnSelectedProductChanged()
        {
            OnPropertyChanged(nameof(SelectedProduct));
        }

        /// <summary>
        /// Поиск соответствий между дочерними продуктами и связями.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private Dictionary<ProductRelation, Product> GetDictionaryOfMatching(Product product)
        {
            var result = new Dictionary<ProductRelation, Product>();
            var actualProductRelations = GetActualProductRelations(product.ProductBlock.Parameters).ToList();
            actualProductRelations.ForEach(x => result.Add(x, default(Product)));

            var dependentProducts = new List<Product>(product.DependentProducts);

            while (dependentProducts.Any())
            {
                var dependentProduct = dependentProducts.First();
                //возможно такой зависимый продукт уже добавлен
                var node = result.Where(x => Equals(x.Value, default(Product))).
                    FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(dependentProduct.ProductBlock.Parameters));
                if (!Equals(node, default(KeyValuePair<ProductRelation, Product>)))
                {
                    result[node.Key] = dependentProduct;
                    dependentProducts.Remove(dependentProduct);
                    continue;
                }

                node = result.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(dependentProduct.ProductBlock.Parameters));

                if (!Equals(node, default(KeyValuePair<ProductRelation, Product>)))
                {
                    dependentProducts.Add(result[node.Key]);
                    result[node.Key] = dependentProduct;
                    dependentProducts.Remove(dependentProduct);
                    continue;
                }

                throw new Exception("Не найдена подходящая зависимость для продукта");
            }

            return result;
        }

        #region events

        public event Action SelectedProductChanged;

        #endregion
    }
}