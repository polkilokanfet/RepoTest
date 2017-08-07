using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly IList<Product> _products;

        public ProductSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Product> products, IEnumerable<Parameter> requiredParameters = null, Product originProduct = null)
        {
            _products = products;

            ParameterSelectors = new ObservableCollection<ParameterSelector>();
            foreach (var parameters in parametersGroups)
            {
                List<Parameter> parametersToSelect = new List<Parameter>();

                var parametersList = parameters as IList<Parameter> ?? parameters.ToList();

                //исключаем возможность выбора параметров, отличных от обязательных
                if (requiredParameters != null && parametersList.Any(requiredParameters.Contains))
                    parametersToSelect.Add(parametersList.Single(requiredParameters.Contains));
                else
                    parametersToSelect.AddRange(parametersList);

                var parameterSelector = new ParameterSelector(parametersToSelect, this);
                ParameterSelectors.Add(parameterSelector);
                //подписываемся на изменение выбора параметра в каждой группе параметров
                parameterSelector.SelectedParameterChanged += ParameterSelectorOnSelectedParameterChanged;
            }

            //выбираем параметры
            if (originProduct != null)
                foreach (var parameter in originProduct.Parameters)
                {
                    var group = ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(parameter));
                    group.SelectedParameter = parameter;
                }

        }


        public ObservableCollection<ParameterSelector> ParameterSelectors { get; set; }

        public ObservableCollection<Parameter> SelectedParameters { get; } = new ObservableCollection<Parameter>();

        public Product SelectedProduct
        {
            get
            {
                var result = _products.SingleOrDefault(x => SelectedParameters.AllMembersAreSame(x.Parameters));
                if (result == null)
                {
                    result = new Product {Parameters = new List<Parameter>(SelectedParameters)};
                    _products.Add(result);
                }
                return result;
            }
        }

        private void RefreshSelectedParameters()
        {
            var actual = ParameterSelectors.Where(x => x.IsActual).Select(x => x.SelectedParameter).ToList();
            var toAdd = actual.Except(SelectedParameters).ToList();
            var toRemove = SelectedParameters.Except(actual).ToList();

            toAdd.ForEach(SelectedParameters.Add);
            toRemove.ForEach(x => SelectedParameters.Remove(x));
        }

        private void ParameterSelectorOnSelectedParameterChanged()
        {
            OnPropertyChanged(nameof(SelectedParameters));
            OnPropertyChanged(nameof(SelectedProduct));
        }
    }
}