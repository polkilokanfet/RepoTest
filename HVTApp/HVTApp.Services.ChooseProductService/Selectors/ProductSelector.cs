using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly IList<Product> _products;
        private Product _selectedProduct;

        public ProductSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Product> products, IEnumerable<Parameter> requiredParameters = null, Product originProduct = null)
        {
            _products = products;

            ParameterSelectors = new ObservableCollection<ParameterSelector>();
            SelectedParameters = new ObservableCollection<Parameter>();
            SelectedParameters.CollectionChanged += SelectedParametersOnCollectionChanged;

            foreach (var parameters in parametersGroups)
            {
                var parameterSelector = new ParameterSelector(parameters);

                //исключаем возможность выбора параметров, отличных от обязательных
                if (requiredParameters != null && parameters.Any(requiredParameters.Contains))
                    parameterSelector = new ParameterSelector(new [] { parameters.Single(requiredParameters.Contains) } );

                ParameterSelectors.Add(parameterSelector);

                //подписываемся на изменение выбора параметра в каждой группе
                parameterSelector.SelectedParameterChanged += OnSelectedParameterChanged;

                if (parameterSelector.SelectedParameter != null) SelectedParameters.Add(parameterSelector.SelectedParameter);
            }

            //выбираем параметры
            if (originProduct != null)
                foreach (var originProductParameter in originProduct.Parameters)
                {
                    var group = ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(originProductParameter));
                    group.SelectedParameter = originProductParameter;
                }
        }

        private void SelectedParametersOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            //перепроверяем флаги актуальности каждого параметра
            foreach (var parameterSelector in ParameterSelectors)
            {
                
                foreach (var parameterWithActualFlag in parameterSelector.ParametersWithActualFlag)
                {
                    parameterWithActualFlag.IsActual =
                        !parameterWithActualFlag.Parameter.RequiredPreviousParameters.Any() ||
                        parameterWithActualFlag.Parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(SelectedParameters));
                }
            }

            SelectedProduct = GetProduct();
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; set; }

        public ObservableCollection<Parameter> SelectedParameters { get; }

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (Equals(_selectedProduct, value)) return;
                var oldValue = _selectedProduct;
                _selectedProduct = value;

                OnSelectedProductChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private Product GetProduct()
        {
            var result = _products.SingleOrDefault(x => SelectedParameters.AllMembersAreSame(x.Parameters));
            if (result == null)
            {
                result = new Product {Parameters = new List<Parameter>(SelectedParameters)};
                _products.Add(result);
            }
            return result;
        }

        private void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            if (oldParameter != null && SelectedParameters.Contains(oldParameter)) SelectedParameters.Remove(oldParameter);
            if (newParameter != null && !SelectedParameters.Contains(newParameter)) SelectedParameters.Add(newParameter);
        }

        private void RefreshActualFlagsOfParameters()
        {
        }

        public event Action<Product, Product> SelectedProductChanged;

        protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            SelectedProductChanged?.Invoke(oldProduct, newProduct);
        }
    }
}