using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly IList<Product> _products;

        public ProductSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Product> products, IEnumerable<Parameter> requiredParameters)
        {
            _products = products;

            SelectedParameters = new ObservableCollection<Parameter>();

            ParametersSelectors = new ObservableCollection<ParametersSelector>();
            foreach (var parameters in parametersGroups)
            {
                var parametersGroup = new ParametersSelector(parameters, null, this);
                ParametersSelectors.Add(parametersGroup);
                //подписываемся на изменение выбора параметра в каждой группе параметров
                parametersGroup.PropertyChanged += ParametersGroupOnPropertyChanged;
            }

            //исключаем возможность выбора параметров, отличных от обязательных
            foreach (var requiredParameter in requiredParameters)
            {
                var group = ParametersSelectors.Single(x => x.ParameterSelectors.Select(p => p.Parameter).Contains(requiredParameter));
                group.SelectedParameter = requiredParameter;
                foreach (var parameterToSelect in group.ParameterSelectors)
                {
                    if (!Equals(parameterToSelect.Parameter, requiredParameter)) group.ParameterSelectors.Remove(parameterToSelect);
                }
            }
        }

        public ObservableCollection<ParametersSelector> ParametersSelectors { get; set; }

        public ObservableCollection<Parameter> SelectedParameters { get; }

        public Product SelectedProduct
        {
            get
            {
                var result = _products.SingleOrDefault(x => SelectedParameters.All(p => x.Parameters.Contains(p)));
                if (result == null)
                {
                    result = new Product() {Parameters = new List<Parameter>(SelectedParameters)};
                    _products.Add(result);
                }
                return result;
            }
        }


        private void ParametersGroupOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ParametersSelector parametersSelector = sender as ParametersSelector;
            if (parametersSelector.IsActual)
            {
                if (!SelectedParameters.Contains(parametersSelector.SelectedParameter)) SelectedParameters.Add(parametersSelector.SelectedParameter);
            }
            else
            {
                if (SelectedParameters.Contains(parametersSelector.SelectedParameter)) SelectedParameters.Remove(parametersSelector.SelectedParameter);
            }

            OnPropertyChanged(nameof(SelectedProduct));
        }
    }
}