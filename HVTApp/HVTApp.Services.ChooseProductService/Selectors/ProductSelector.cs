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
        private readonly IEnumerable<RequiredDependentProductsParameters> _requiredDependentProductsParametersList;

        private Product _selectedProduct;

        public PartSelector PartSelector { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; }


        public ProductSelector(IEnumerable<ParameterGroup> groups,
                                 IList<Part> parts,
                                 IList<Product> products,
                                 IEnumerable<RequiredDependentProductsParameters> requiredDependentProductsParametersList,
                                 IEnumerable<Parameter> requiredProductsParameters = null,
                                 Product preSelectedProduct = null)
        {
            _groups = new List<ParameterGroup>(groups);
            _parts = parts;
            _products = products;
            _requiredDependentProductsParametersList = new List<RequiredDependentProductsParameters>(requiredDependentProductsParametersList);

            //�������
            PartSelector = new PartSelector(_groups.Select(x => x.Parameters), _parts, requiredProductsParameters, preSelectedProduct?.Part);
            PartSelector.SelectedPartChanged += OnMainPartChanged;

            //�������� ������������
            ProductSelectors = new ObservableCollection<ProductSelector>();
            RefreshDependentProducts();
            RefreshSelectedProduct();
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
                DependentProducts = ProductSelectors.Select(p => p.SelectedProduct).ToList()
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

        private void RefreshDependentProducts()
        {
            // ���������, ����������� ���������� ������������
            var RequiredDependentProductParameters =_requiredDependentProductsParametersList
                .Where(x => x.MainProductParameters.AllContainsIn(PartSelector.SelectedParameters));

            //��������� �� ���������� �������� ������������
            foreach (var productSelector in ProductSelectors
                .Where(ps => !RequiredDependentProductParameters.Any(x => x.ChildProductParameters.AllMembersAreSame(ps.PartSelector.GetRequaredParameters()))).ToList())
            {
                ProductSelectors.Remove(productSelector);
                productSelector.SelectedProductChanged -= OnDependentProductChanged;
            }

            //��������� ���������� �������� ������������
            foreach (var requiredDependentProductParameters in RequiredDependentProductParameters
                .Where(x => !ProductSelectors.Any(des => des.PartSelector.GetRequaredParameters().AllMembersAreSame(x.ChildProductParameters))))
            {
                for (int i = 0; i < requiredDependentProductParameters.Count; i++)
                {
                    var productSelector = new ProductSelector(_groups, _parts, _products, _requiredDependentProductsParametersList, requiredDependentProductParameters.ChildProductParameters);
                    ProductSelectors.Add(productSelector);
                    productSelector.SelectedProductChanged += OnDependentProductChanged;
                }
            }
        }

        private void OnDependentProductChanged(Product oldProduct, Product newProduct)
        {
            RefreshSelectedProduct();
        }

        private void OnMainPartChanged(Part oldPart, Part newPart)
        {
            RefreshDependentProducts();
            RefreshSelectedProduct();
        }


        public event Action<Product, Product> SelectedProductChanged;
        protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            SelectedProductChanged?.Invoke(oldProduct, newProduct);
        }
    }
}