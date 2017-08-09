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

        public ProductSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Product> products, IEnumerable<Parameter> requiredParameters = null, Product preSelectedProduct = null)
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

            //назаначаем предварительно выбранный продукт
            if (preSelectedProduct != null) SelectedProduct = preSelectedProduct;
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

                var actualParameterSelectors = new List<ParameterSelector>();
                foreach (var parameter in _selectedProduct.Parameters)
                {
                    var parameterSelector = ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(parameter));
                    if (!Equals(parameterSelector.SelectedParameter, parameter)) parameterSelector.SelectedParameter = parameter;
                    actualParameterSelectors.Add(parameterSelector);
                }
                foreach (var parameterSelector in ParameterSelectors.Except(actualParameterSelectors))
                {
                    if (!Equals(parameterSelector.SelectedParameter, null)) parameterSelector.SelectedParameter = null;
                }

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
            SelectedParameters.CollectionChanged -= SelectedParametersOnCollectionChanged;

            //определяем выбранные параметры, которые зависели от старого параметра
            List<Parameter> dependendParametersFromOld = new List<Parameter>();
            if (oldParameter != null)
                dependendParametersFromOld.AddRange(SelectedParameters.Where(x => ParameterNeeds(x, oldParameter)));
            //определяем выбранные параметры, которые зависят от нового параметра
            List<Parameter> dependendParametersFromNew = new List<Parameter>();
            if (newParameter != null)
                dependendParametersFromNew.AddRange(SelectedParameters.Where(x => ParameterNeeds(x, newParameter)));

            //исключаем не актуальные параметры параметры
            dependendParametersFromOld.Except(dependendParametersFromNew).ToList().ForEach(x => SelectedParameters.Remove(x));

            SelectedParameters.CollectionChanged += SelectedParametersOnCollectionChanged;

            if (oldParameter != null && SelectedParameters.Contains(oldParameter)) SelectedParameters.Remove(oldParameter);
            if (newParameter != null && !SelectedParameters.Contains(newParameter)) SelectedParameters.Add(newParameter);
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

        //нужен ли параметру для актуальности выбор другого параметра
        private bool ParameterNeeds(Parameter targetParameter, Parameter possibleNeededParameter)
        {
            if (!targetParameter.RequiredPreviousParameters.Any()) return false;
            return targetParameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.Contains(possibleNeededParameter));
        }

        public event Action<Product, Product> SelectedProductChanged;

        protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            SelectedProductChanged?.Invoke(oldProduct, newProduct);
        }
    }
}