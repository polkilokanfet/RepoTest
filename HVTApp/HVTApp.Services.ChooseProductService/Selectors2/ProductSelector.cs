using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector
    {
        public ProductRelation ProductRelation { get; }

        private readonly IEnumerable<Product> _allProducts;
        private readonly IEnumerable<ProductRelation> _productRelations;
        private readonly List<Parameter> _allParameters;

        public ProductSelector(IEnumerable<Product> allProducts, IEnumerable<Parameter> allParameters, IEnumerable<ProductRelation> productRelations, ProductRelation productRelation = null)
        {
            _allParameters = allParameters.ToList();
            _allProducts = allProducts;
            _productRelations = productRelations;
            ProductRelation = productRelation;

            var parametersGrouped = _allParameters.GroupBy(x => x.GroupId);
            foreach (var group in parametersGrouped)
            {
                IEnumerable<Parameter> parameters = group;

                if (productRelation != null)
                {
                    var intersect = group.Intersect(productRelation.ChildProductParameters).ToList();
                    if (intersect.Any())
                        parameters = intersect;
                }

                var parameterSelector = new ParameterSelector(parameters, this);

                parameterSelector.SelectedParameterChanged += ParameterSelectorOnSelectedParameterChanged;
                parameterSelector.IsActualChanged += ParameterSelectorOnIsActualChanged;

                if(parameterSelector.IsActual)
                    ParameterSelectorOnIsActualChanged(parameterSelector);
            }

            RefreshDependentProducts();
        }

        public Product Product
        {
            get
            {
                var product = new Product
                {
                    Parameters = SelectedParameters.ToList(),
                    DependentProducts = DependentProductSelectors.Select(x => x.Product).ToList()
                };

                var result = _allProducts.SingleOrDefault(x => x.Equals(product));
                return result ?? product;
            }
        }

        public event Action ProductChanged;

        private void ParameterSelectorOnIsActualChanged(ParameterSelector parameterSelector)
        {
            if (parameterSelector.IsActual)
                ParameterSelectors.Add(parameterSelector);
            else
                ParameterSelectors.Remove(parameterSelector);
        }

        private void ParameterSelectorOnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            OnSelectedParametersChanged();
            RefreshDependentProducts();
            OnProductChanged();
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; } = new ObservableCollection<ParameterSelector>();

        public IEnumerable<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameter);

        public event Action<IEnumerable<Parameter>> SelectedParametersChanged;



        private readonly List<Parameter> _selectedParametersBefore = new List<Parameter>();
        protected virtual void OnSelectedParametersChanged()
        {
            if (_selectedParametersBefore.AllMembersAreSame(SelectedParameters)) return;

            _selectedParametersBefore.Clear();
            _selectedParametersBefore.AddRange(SelectedParameters);

            SelectedParametersChanged?.Invoke(SelectedParameters);
        }

        public ObservableCollection<ProductSelector> DependentProductSelectors { get; set; } = new ObservableCollection<ProductSelector>();

        protected virtual void OnProductChanged()
        {
            ProductChanged?.Invoke();
        }



        private IEnumerable<ProductRelation> ActualProductRelations => _productRelations.Where(x => x.ParentProductParameters.AllContainsIn(SelectedParameters));

        private void RefreshDependentProducts()
        {
            var removeProductSelectors = new List<ProductSelector>(DependentProductSelectors);
            var originProductSelectors = new List<ProductSelector>(DependentProductSelectors);

            foreach (var actualProductRelation in ActualProductRelations)
            {
                for (int i = 0; i < actualProductRelation.Count; i++)
                {

                    if (removeProductSelectors.Any(x => Equals(x.ProductRelation, actualProductRelation)))
                    {
                        var actualProductSelector = removeProductSelectors.First(x => Equals(x.ProductRelation, actualProductRelation));
                        removeProductSelectors.Remove(actualProductSelector);
                    }
                    else
                    {
                        var productSelector = new ProductSelector(_allProducts, _allParameters, _productRelations, actualProductRelation);
                        DependentProductSelectors.Add(productSelector);
                    }
                }
            }

            removeProductSelectors.ForEach(x => DependentProductSelectors.Remove(x));

            if (!DependentProductSelectors.AllMembersAreSame(originProductSelectors))
                OnProductChanged();
        }
    }
}