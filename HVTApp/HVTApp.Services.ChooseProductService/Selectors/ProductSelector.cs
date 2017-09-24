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
        private readonly IEnumerable<RequiredDependentProductsParameters> _requiredDependentProductsParametersList;

        private readonly Dictionary<RequiredDependentProductsParameters, IEnumerable<ProductSelector>> _productSelectorsDictionary = 
            new Dictionary<RequiredDependentProductsParameters, IEnumerable<ProductSelector>>();

        public PartSelector PartSelector { get; }

        public IEnumerable<ProductSelector> ProductSelectors => 
            _productSelectorsDictionary
            .Where(x => x.Key.MainProductParameters.AllContainsIn(PartSelector.SelectedPart.Parameters))
            .SelectMany(x => x.Value);


        public ProductSelector(IEnumerable<Parameter> parameters, 
                               IEnumerable<RequiredDependentProductsParameters> requiredDependentProductsParametersList,
                               IEnumerable<Parameter> requiredParameters = null,
                               Product preSelectedProduct = null)
        {
            _requiredDependentProductsParametersList = new List<RequiredDependentProductsParameters>(requiredDependentProductsParametersList);

            PartSelector = new PartSelector(requiredParameters ?? parameters, preSelectedProduct?.Part);
            PartSelector.SelectedPartChanged += OnMainPartChanged;

            foreach (var requiredDependentProductsParameters in requiredDependentProductsParametersList)
            {
                var productSelectors = new List<ProductSelector>();
                var childProductParameters = requiredDependentProductsParameters.ChildProductParameters;
                var exceptedGroups = childProductParameters.Select(x => x.Group);
                var requiredProductSelectorsParameters = parameters.Where(x => (childProductParameters.Contains(x) || !exceptedGroups.Contains(x.Group)));
                for (int i = 0; i < requiredDependentProductsParameters.Count; i++)
                    productSelectors.Add(new ProductSelector(parameters, requiredDependentProductsParametersList, requiredProductSelectorsParameters));

                _productSelectorsDictionary.Add(requiredDependentProductsParameters, productSelectors);
            }

            if (preSelectedProduct != null) SelectedProduct = preSelectedProduct;
        }


        public Product SelectedProduct
        {
            get
            {
                return new Product
                {
                    Part = PartSelector.SelectedPart,
                    DependentProducts = ProductSelectors.Select(p => p.SelectedProduct).ToList()
                };
            }
            set
            {
                PartSelector.SelectedPart = value.Part;

                var targetPairs =
                    _productSelectorsDictionary.Where(x => x.Key.MainProductParameters.AllContainsIn(value.Part.Parameters)).ToList();

                List<ProductSelector> usedProductSelectors = new List<ProductSelector>();
                foreach (var dependentProduct in value.DependentProducts)
                {
                    var targetPair = targetPairs.First(x => x.Key.ChildProductParameters.AllContainsIn(dependentProduct.Part.Parameters));
                    var ps = targetPair.Value.First(x => !usedProductSelectors.Contains(x));
                    ps.SelectedProduct = dependentProduct;
                    usedProductSelectors.Add(ps);
                }

                OnPropertyChanged();
                OnSelectedProductChanged();
            }
        }

        private void OnDependentProductChanged(Product oldProduct, Product newProduct)
        {
            OnPropertyChanged(nameof(SelectedProduct));
        }

        private void OnMainPartChanged()
        {
            OnPropertyChanged(nameof(ProductSelectors));
            OnPropertyChanged(nameof(SelectedProduct));
        }


        public event Action SelectedProductChanged;
        protected virtual void OnSelectedProductChanged()
        {
            SelectedProductChanged?.Invoke();
        }
    }


    //public class ProductSelector : NotifyPropertyChanged
    //{
    //    private readonly IEnumerable<ParameterGroup> _groups;
    //    private readonly IList<Part> _parts;
    //    private readonly IList<Product> _products;
    //    private readonly IEnumerable<RequiredDependentProductsParameters> _requiredDependentProductsParametersList;

    //    private Product _selectedProduct;

    //    public PartSelector PartSelector { get; }
    //    public ObservableCollection<ProductSelector> ProductSelectors { get; }


    //    public ProductSelector(  IEnumerable<ParameterGroup> groups, 
    //                             IList<Part> parts, 
    //                             IList<Product> products, 
    //                             IEnumerable<RequiredDependentProductsParameters> requiredDependentProductsParametersList, 
    //                             IEnumerable<Parameter> requiredProductsParameters = null,
    //                             Product preSelectedProduct = null)
    //    {
    //        _groups = new List<ParameterGroup>(groups);
    //        _parts = parts;
    //        _products = products;
    //        _requiredDependentProductsParametersList = new List<RequiredDependentProductsParameters>(requiredDependentProductsParametersList);

    //        //продукт
    //        PartSelector = new PartSelector(_groups.Select(x => x.Parameters), _parts, requiredProductsParameters, preSelectedProduct?.Part);
    //        PartSelector.SelectedPartChanged += OnMainPartChanged;

    //        //дочернее оборудование
    //        ProductSelectors = new ObservableCollection<ProductSelector>();
    //        RefreshDependentProducts();
    //        SelectedProduct = GetProduct();
    //    }


    //    public Product SelectedProduct
    //    {
    //        get { return _selectedProduct; }
    //        private set
    //        {
    //            if (Equals(_selectedProduct, value)) return;
    //            var oldValue = _selectedProduct;
    //            _selectedProduct = value;
    //            if(!_products.Contains(value)) _products.Add(value);

    //            OnSelectedProductChanged(oldValue, value);
    //            OnPropertyChanged();
    //        }
    //    }

    //    private Product GetProduct()
    //    {
    //        var selectedProduct = new Product
    //        {
    //            Part = PartSelector.SelectedPart,
    //            DependentProducts = ProductSelectors.Select(p => p.SelectedProduct).ToList()
    //        };

    //        var result = _products.SingleOrDefault(x => x.Equals(selectedProduct)) ?? selectedProduct;
    //        if (!_products.Contains(result)) _products.Add(result);
    //        return result;
    //    }

    //    /// <summary>
    //    /// Параметры, необходимые зависимому оборудованию
    //    /// </summary>
    //    IEnumerable<RequiredDependentProductsParameters> RequiredDependentProductParameters => 
    //        _requiredDependentProductsParametersList
    //        .Where(x => x.MainProductParameters.AllContainsIn(PartSelector.SelectedParameters));

    //    private void RefreshDependentProducts()
    //    {
    //        //исключаем не актуальное дочернее оборудование
    //        foreach (var productSelector in ProductSelectors
    //            .Where(ps => !RequiredDependentProductParameters.Any(x => x.ChildProductParameters.AllMembersAreSame(ps.PartSelector.GetRequaredParameters()))).ToList())
    //        {
    //            ProductSelectors.Remove(productSelector);
    //            productSelector.SelectedProductChanged -= OnDependentProductChanged;
    //        }

    //        //добавляем актуальное дочернее оборудование
    //        foreach (var requiredDependentProductParameters in RequiredDependentProductParameters
    //            .Where(x => !ProductSelectors.Any(des => des.PartSelector.GetRequaredParameters().AllMembersAreSame(x.ChildProductParameters))))
    //        {
    //            for (int i = 0; i < requiredDependentProductParameters.Count; i++)
    //            {
    //                var productSelector = new ProductSelector(_groups, _parts, _products, _requiredDependentProductsParametersList, requiredDependentProductParameters.ChildProductParameters);
    //                ProductSelectors.Add(productSelector);
    //                productSelector.SelectedProductChanged += OnDependentProductChanged;
    //            }
    //        }
    //    }

    //    private void OnDependentProductChanged(Product oldProduct, Product newProduct)
    //    {
    //        SelectedProduct = GetProduct();
    //    }

    //    private void OnMainPartChanged(Part oldPart, Part newPart)
    //    {
    //        RefreshDependentProducts();
    //        SelectedProduct = GetProduct();
    //    }


    //    public event Action<Product, Product> SelectedProductChanged;
    //    protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
    //    {
    //        SelectedProductChanged?.Invoke(oldProduct, newProduct);
    //    }
    //}
}