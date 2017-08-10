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
        private readonly IList<Product> _products;
        private Product _selectedProduct;

        public ProductSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Product> products, IEnumerable<Parameter> requiredParameters = null, Product preSelectedProduct = null)
        {
            _products = products;

            ParameterSelectors = new ObservableCollection<ParameterSelector>();

            foreach (var parameters in parametersGroups)
            {
                var parameterSelector = new ParameterSelector(parameters);

                //исключаем возможность выбора параметров, отличных от обязательных
                if (requiredParameters != null && parameters.Any(requiredParameters.Contains))
                    parameterSelector = new ParameterSelector(new [] { parameters.Single(requiredParameters.Contains) } );

                ParameterSelectors.Add(parameterSelector);

                //подписываемся на изменение выбора параметра в каждой группе
                parameterSelector.SelectedParameterChanged += OnSelectedParameterChanged;
                //перепроверяем статусы актуальности каждого параметра
                RefreshParametersActualStatuses(SelectedParameters);
            }

            SelectedProduct = GetProduct();

            //назаначаем предварительно выбранный продукт
            if (preSelectedProduct != null) SelectedProduct = preSelectedProduct;
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; set; }

        public IEnumerable<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameter).Where(x => x != null);

        public Product SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                if (Equals(_selectedProduct, value)) return;
                var oldValue = _selectedProduct;
                _selectedProduct = value;

                //назначаем в селекторы актуальные выбранные параметры
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

        private void RefreshParametersActualStatuses(IEnumerable<Parameter> actualSelectedParameters)
        {
            //перепроверяем флаги актуальности каждого параметра
            foreach (var parameterSelector in ParameterSelectors)
            {
                foreach (var parameterWithActualFlag in parameterSelector.ParametersWithActualFlag)
                    parameterWithActualFlag.IsActual = ParameterIsActual(parameterWithActualFlag.Parameter, actualSelectedParameters);

                var selectedParameterWithActualFlag = parameterSelector.ParametersWithActualFlag.SingleOrDefault(x => Equals(x.Parameter, parameterSelector.SelectedParameter));

                //если выбранный параметр потерял свою актуальность, выбирам другой актуальный
                if (selectedParameterWithActualFlag != null && !selectedParameterWithActualFlag.IsActual)
                    parameterSelector.SelectedParameter = parameterSelector.ParametersWithActualFlag.FirstOrDefault(x => x.IsActual)?.Parameter;
            }
        }


        private void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            //определяем выбранные параметры, которые зависели от старого параметра
            List<Parameter> dependendParametersFromOld = new List<Parameter>();
            if (oldParameter != null)
                dependendParametersFromOld.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, oldParameter)));
            //определяем выбранные параметры, которые зависят от нового параметра
            List<Parameter> dependendParametersFromNew = new List<Parameter>();
            if (newParameter != null)
                dependendParametersFromNew.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, newParameter)));

            //не актуальные параметры
            var notActualSelectedParameters = dependendParametersFromOld.Except(dependendParametersFromNew);
            //актуальные параметры
            var actualSelectedParameters = SelectedParameters.Except(notActualSelectedParameters).ToList();
            if (oldParameter != null) actualSelectedParameters.Remove(oldParameter);
            if (newParameter != null) actualSelectedParameters.Add(newParameter);

            //перепроверяем флаги актуальности каждого параметра
            RefreshParametersActualStatuses(actualSelectedParameters);
            OnSelectedParametersChanged();

            SelectedProduct = GetProduct();
        }


        private bool ParameterIsActual(Parameter parameter, IEnumerable<Parameter> requiredParameters)
        {
            return !parameter.RequiredPreviousParameters.Any() ||
                    parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(requiredParameters));
        }

        //нужен ли параметру для актуальности выбор другого параметра
        private bool ParameterDependsFrom(Parameter targetParameter, Parameter possibleNeededParameter)
        {
            if (!targetParameter.RequiredPreviousParameters.Any()) return false;
            return targetParameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.Contains(possibleNeededParameter));
        }





        public event Action<Product, Product> SelectedProductChanged;

        protected virtual void OnSelectedProductChanged(Product oldProduct, Product newProduct)
        {
            SelectedProductChanged?.Invoke(oldProduct, newProduct);
        }

        private event Action SelectedParametersChanged;

        protected virtual void OnSelectedParametersChanged()
        {
            SelectedParametersChanged?.Invoke();
        }
    }
}