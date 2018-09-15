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

        public ProductBlockSelector ProductBlockSelector { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; } = new ObservableCollection<ProductSelector>();
        public int Amount { get; }
        public bool HasDependentProducts => ProductSelectors.Any();

        public IEnumerable<ProductDependent> ProductDependents
        {
            get
            {
                //берем продукты, выбранные в селекторе
                var dps = ProductSelectors.Select(x => x.SelectedProduct);
                //находим повторяющиеся (группируем).
                var gdps = dps.GroupBy(product => product, new ProductComparer());
                //генерируем зависимые продукты
                foreach (var gdp in gdps)
                {
                    yield return new ProductDependent {Product = gdp.Key, Amount = gdp.Count()};
                }
            } 
        }

        public Product SelectedProduct
        {
            get
            {
                var product = new Product
                {
                    ProductBlock = ProductBlockSelector.SelectedBlock,
                    DependentProducts = ProductDependents.ToList()
                };
                //если такой продукт существует - возвращаем его
                var existsProduct = _bank.Products.SingleOrDefault(x => x.Equals(product));
                if (existsProduct != null) return existsProduct;

                //если продукт еще не существовал
                //обозначение и тип нового продукта
                product.Designation = _bank.Designator.GetDesignation(product);
                product.ProductType = _bank.Designator.GetProductType(product);

                _bank.Products.Add(product);
                return product;
            }
        }

        public ProductSelector(Bank bank, IEnumerable<Parameter> parameters, Product selectedProduct = null, int amount = 1)
        {
            if (bank == null) throw new ArgumentNullException(nameof(bank));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            _bank = bank;

            Amount = amount;
            ProductBlockSelector = new ProductBlockSelector(parameters, _bank, selectedProduct?.ProductBlock);
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
                    if (Equals(kvp.Value, default(IEnumerable<Product>)))
                        continue;
                    foreach (var product in kvp.Value)
                    {
                        //редактируем список параметров
                        var usefullParameters = bank.Parameters.GetUsefull(kvp.Key.ChildProductParameters);
                        var productSelector = new ProductSelector(bank, usefullParameters, product);
                        ProductSelectors.Add(productSelector);
                        productSelector.SelectedProductChanged += ProductSelectorOnSelectedProductChanged;
                    }
                }
            }
        }

        private List<ProductRelation> GetActualProductRelations(IEnumerable<Parameter> forParameters = null)
        {
            var parameters = forParameters ?? ProductBlockSelector.SelectedBlock.Parameters;
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
                               productSelector.ProductBlockSelector.SelectedBlock.Parameters));

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
                    var productSelector = new ProductSelector(_bank, _bank.Parameters.GetUsefull(productRelation.ChildProductParameters));
                    ProductSelectors.Add(productSelector);
                    productSelector.SelectedProductChanged += ProductSelectorOnSelectedProductChanged;
                }
            }

            OnPropertyChanged(nameof(HasDependentProducts));
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
        private Dictionary<ProductRelation, IEnumerable<Product>> GetDictionaryOfMatching(Product product)
        {
            var result = new Dictionary<ProductRelation, IEnumerable<Product>>();
            //получаем актуальные для выбранных параметров связи
            var actualProductRelations = GetActualProductRelations(product.ProductBlock.Parameters).ToList();
            actualProductRelations.ForEach(x => result.Add(x, default(IEnumerable<Product>)));

            //составляем список дочерних продуктов
            var dependentProducts = new List<Product>();
            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    dependentProducts.Add(dependentProduct.Product);
                }
            }

            //составляем словарь соответствий
            while (dependentProducts.Any())
            {
                var dependentProduct = dependentProducts.First();
                //возможно такой зависимый продукт уже добавлен
                //ищем ключ, список дочерних параметров которого полностью содержится в списке параметров дочернего блока и он еще не полный (количество дочерних продуктов)
                var node = result.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(dependentProduct.ProductBlock.Parameters) && 
                                                     (x.Value == null || x.Value.Count() < x.Key.ChildProductsAmount));
                if (!Equals(node, default(KeyValuePair<ProductRelation, IEnumerable<Product>>)))
                {
                    //добавляем дочерний продукт в словарь
                    if(result[node.Key] == null)
                        result[node.Key] = new List<Product>();
                    ((List<Product>)result[node.Key]).Add(dependentProduct);
                    
                    //исключаем его из дальнейшего поиска соответствий
                    dependentProducts.Remove(dependentProduct);
                    continue;
                }


                node = result.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(dependentProduct.ProductBlock.Parameters));

                if (!Equals(node, default(KeyValuePair<ProductRelation, IEnumerable<Product>>)))
                {
                    dependentProducts.Add(result[node.Key].Last());
                    ((List<Product>)result[node.Key]).Add(dependentProduct);
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