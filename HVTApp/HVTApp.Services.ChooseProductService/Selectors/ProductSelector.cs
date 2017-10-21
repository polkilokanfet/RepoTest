using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<ParameterGroup> _groups;
        private readonly IList<Part> _parts;
        private readonly IList<Product> _products;
        private readonly IEnumerable<Parameter> _requiredParameters;
        private readonly IEnumerable<ProductsRelation> _productsRelations;

        private Product _selectedProduct;
        private bool _isActual = true;

        public PartSelector PartSelector { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; } = new ObservableCollection<ProductSelector>();

        private readonly Dictionary<ProductsRelation, IEnumerable<ProductSelector>> _productSelectorsDictionary 
            = new Dictionary<ProductsRelation, IEnumerable<ProductSelector>>();


        public ProductSelector(IEnumerable<ParameterGroup> groups,
                               IList<Part> parts,
                               IList<Product> products,
                               IEnumerable<ProductsRelation> productsRelations,
                               IEnumerable<Parameter> requiredParameters = null,
                               Product preSelectedProduct = null)
        {
            _groups = groups;
            _parts = parts;
            _products = products;
            _requiredParameters = requiredParameters;
            _productsRelations = new List<ProductsRelation>(productsRelations);

            //продукт
            PartSelector = new PartSelector(_groups.Select(x => x.Parameters), _parts, requiredParameters, preSelectedProduct?.Part);
            PartSelector.SelectedPartChanged += OnMainPartChanged;

            GenerateProductSelectors();

            //дочернее оборудование
            RefreshProductSelectorsActualStatuses();
            RefreshSelectedProduct();
        }

        private void GenerateProductSelectors()
        {
            //связи, актуальные при таких обязательных к выбору параметров
            var relations = _requiredParameters == null
                ? _productsRelations
                : _productsRelations.Where(x => x.ParentProductParameters.AllContainsIn(_requiredParameters));

            //создаём словарь селекторов продукта
            foreach (var relation in relations)
            {
                var productSelectors = new List<ProductSelector>();
                for (int i = 0; i < relation.Count; i++)
                {
                    var productSelector = new ProductSelector(_groups, _parts, _products, _productsRelations, relation.ChildProductParameters);
                    productSelectors.Add(productSelector);
                    ProductSelectors.Add(productSelector);
                }
                _productSelectorsDictionary.Add(relation, productSelectors);
            }
            //подписываемся на событие изменение дочернего продукта
            ProductSelectors.ToList().ForEach(x => x.SelectedProductChanged += OnDependentProductChanged);
        }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            private set
            {
                if (Equals(_selectedProduct, value)) return;
                var oldValue = _selectedProduct;
                _selectedProduct = value;
                if (!_products.Contains(value)) _products.Add(value);

                OnSelectedProductChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private void RefreshSelectedProduct()
        {
            var selectedProduct = new Product
            {
                Part = PartSelector.SelectedPart,
                DependentProducts = ProductSelectors.Where(x => x.IsActual).Select(p => p.SelectedProduct).ToList()
            };

            var result = _products.SingleOrDefault(x => ProductsAreSame(x, selectedProduct)) ?? selectedProduct;
            if (!_products.Contains(result)) _products.Add(result);
            SelectedProduct = result;
        }

        private bool ProductsAreSame(Product firstProduct, Product secondProduct)
        {
            if(firstProduct == null || secondProduct == null) throw new ArgumentNullException();

            if (!firstProduct.Part.Parameters.AllMembersAreSame(secondProduct.Part.Parameters)) return false;

            if (firstProduct.DependentProducts.Count != secondProduct.DependentProducts.Count) return false;

            foreach (var dependentProduct in firstProduct.DependentProducts)
                if (secondProduct.DependentProducts.Any(x => ProductsAreSame(x, dependentProduct))) return false;

            foreach (var dependentProduct in secondProduct.DependentProducts)
                if (firstProduct.DependentProducts.Any(x => ProductsAreSame(x, dependentProduct))) return false;

            return true;
        }

        public bool IsActual
        {
            get { return _isActual; }
            set
            {
                if (_isActual == value) return;
                _isActual = value;
                OnIsActualChanged();
                OnPropertyChanged();
            }
        }

        private void RefreshProductSelectorsActualStatuses()
        {
            var actualProductsRelations =_productsRelations
                .Where(x => x.ParentProductParameters.AllContainsIn(PartSelector.SelectedParameters));

            _productSelectorsDictionary.SelectMany(x => x.Value).ToList().ForEach(x => x.IsActual = false);
            actualProductsRelations.ToList()
                .ForEach(x => _productSelectorsDictionary[x].ToList().ForEach(ps => ps.IsActual = true));
        }

        private void OnDependentProductChanged(Product oldProduct, Product newProduct)
        {
            RefreshSelectedProduct();
        }

        private void OnMainPartChanged(Part oldPart, Part newPart)
        {
            RefreshProductSelectorsActualStatuses();
            RefreshSelectedProduct();
        }


        public event Action<Product, Product> SelectedProductChanged;
        protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            SelectedProductChanged?.Invoke(oldProduct, newProduct);
        }

        public event Action IsActualChanged;
        protected virtual void OnIsActualChanged()
        {
            IsActualChanged?.Invoke();
        }

    }
}