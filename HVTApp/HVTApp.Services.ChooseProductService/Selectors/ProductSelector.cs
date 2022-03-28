using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private Bank Bank { get; }

        public ProductBlockSelector BlockSelector { get; }

        /// <summary>
        /// ��������� �������� ���������
        /// </summary>
        public ObservableCollection<ProductSelector> ProductSelectors { get; } = new ObservableCollection<ProductSelector>();

        public int Amount { get; }
        public bool HasDependentProducts => ProductSelectors.Any();

        public IEnumerable<ProductDependent> ProductDependents
        {
            get
            {
                //����� ��������, ��������� � ���������
                var dps = ProductSelectors.Select(x => x.SelectedProduct);
                //������� ������������� (����������).
                var gdps = dps.GroupBy(product => product, new ProductComparer());
                //���������� ��������� ��������
                foreach (var gdp in gdps)
                {
                    yield return new ProductDependent {Product = gdp.Key, Amount = gdp.Count()};
                }
            } 
        }

        public Product SelectedProduct => Bank.GetProduct(BlockSelector.SelectedBlock, ProductDependents);

        public ProductSelector(Bank bank, IEnumerable<Parameter> parameters, Product selectedProduct = null, int amount = 1)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));

            Bank = bank;
            Amount = amount;

            //������� �������� �����
            BlockSelector = new ProductBlockSelector(parameters, Bank, selectedProduct?.ProductBlock);
            //������������� �� ������� ��� ���������
            BlockSelector.SelectedBlockChanged += selector =>
            {
                RefreshProductSelectors();
                SelectedProductChanged?.Invoke();
                OnPropertyChanged(nameof(SelectedProduct));
            };

            //��������/���������� ���������� �������� ���������
            ProductSelectors.CollectionChanged += (sender, args) =>
            {
                args.NewItems?.Cast<ProductSelector>().ForEach(x => x.SelectedProductChanged += OnChildProductChanged);
                args.OldItems?.Cast<ProductSelector>().ForEach(x => x.SelectedProductChanged -= OnChildProductChanged);
            };

            if (selectedProduct == null)
            {
                //����� ���������, ����������� ������� ���������
                var parameterSelector = BlockSelector.ParameterSelectors.Single(x => x.ParametersFlaged.All(p => p.Parameter.IsOrigin));
                //����� ���������
                parameterSelector.SelectedParameterFlaged = parameterSelector.ParametersFlaged.First();
            }
            else
            {
                foreach (var kvp in GetDictionaryOfMatching(selectedProduct))
                {
                    if (Equals(kvp.Value, default(IEnumerable<Product>))) continue;

                    foreach (var product in kvp.Value)
                    {
                        //����������� ������ ����������
                        var usefullParameters = bank.Parameters.GetUsefull(kvp.Key);
                        var productSelector = new ProductSelector(bank, usefullParameters, product);
                        ProductSelectors.Add(productSelector);
                    }
                }
            }
        }

        private void RefreshProductSelectors()
        {
            //������������� �������� ��������� �� ���������� ���������� ���������� � �����
            var productSelectors = ProductSelectors.OrderByDescending(x => x.SelectedProduct.ProductBlock.Parameters.Count).ToList();

            //��������� ����� � �������� ���������, ������������� �� ���������� ����������, ���������� ��������
            var childProductsRelations = Bank.RelationsToChildProducts(SelectedProduct).OrderBy(x => x.ChildProductParameters.Count).ToList();

            var relaitionsDictionary = new Dictionary<ProductRelation, int>();
            foreach (var actualProductRelation in childProductsRelations)
            {
                relaitionsDictionary.Add(actualProductRelation, actualProductRelation.ChildProductsAmount);
            }

            //�������� ������������ ���������� � ������ ������
            foreach (var productSelector in productSelectors.ToList())
            {
                //���� �����, ������� ������������� ���������
                var relation = childProductsRelations.FirstOrDefault(x => x.ChildProductParameters.AllContainsIn(productSelector.BlockSelector.SelectedBlock.Parameters));

                //���� �� ������� - ������ ��
                if (relation == null)
                {
                    ProductSelectors.Remove(productSelector);
                }
                //���� ������� - ������������ ����� � ������� ���� �������� �� ������
                else
                {
                    productSelectors.Remove(productSelector);

                    relaitionsDictionary[relation] -= productSelector.Amount;
                    if (relaitionsDictionary[relation] == 0)
                    {
                        childProductsRelations.Remove(relation);
                    }
                }
            }

            //���������� ����� ���������� ����������
            foreach (var productRelation in childProductsRelations)
            {
                for (int i = 0; i < relaitionsDictionary[productRelation]; i++)
                {
                    //����� �������� � ���������� ��� ����� �����������
                    var productSelector = new ProductSelector(Bank, Bank.Parameters.GetUsefull(productRelation));
                    ProductSelectors.Add(productSelector);
                }
            }

            OnPropertyChanged(nameof(HasDependentProducts));
        }

        /// <summary>
        /// ������� �� ��������� ��������� ��������
        /// </summary>
        private void OnChildProductChanged()
        {
            OnPropertyChanged(nameof(SelectedProduct));
        }

        /// <summary>
        /// ����� ������������ ����� ��������� ���������� � �������.
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        private Dictionary<ProductRelation, IEnumerable<Product>> GetDictionaryOfMatching(Product product)
        {
            var result = new Dictionary<ProductRelation, IEnumerable<Product>>();
            //�������� ���������� ��� ��������� ���������� �����
            var actualProductRelations = Bank.RelationsToChildProducts(product).ToList();
            actualProductRelations.ForEach(x => result.Add(x, default(IEnumerable<Product>)));

            //���������� ������ �������� ���������
            var dependentProducts = new List<Product>();
            foreach (var dependentProduct in product.DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    dependentProducts.Add(dependentProduct.Product);
                }
            }

            //���������� ������� ������������
            while (dependentProducts.Any())
            {
                var dependentProduct = dependentProducts.First();
                //�������� ����� ��������� ������� ��� ��������
                //���� ����, ������ �������� ���������� �������� ��������� ���������� � ������ ���������� ��������� ����� � �� ��� �� ������ (���������� �������� ���������)
                var node = result.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(dependentProduct.ProductBlock.Parameters) && 
                                                     (x.Value == null || x.Value.Count() < x.Key.ChildProductsAmount));
                if (!Equals(node, default(KeyValuePair<ProductRelation, IEnumerable<Product>>)))
                {
                    //��������� �������� ������� � �������
                    if(result[node.Key] == null)
                        result[node.Key] = new List<Product>();
                    ((List<Product>)result[node.Key]).Add(dependentProduct);
                    
                    //��������� ��� �� ����������� ������ ������������
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

                throw new Exception("�� ������� ���������� ����������� ��� ��������");
            }

            return result;
        }

        #region events

        public event Action SelectedProductChanged;

        #endregion
    }
}