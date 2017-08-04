using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        private readonly IEnumerable<Parameter> _parameters;
        private readonly ProductSelector _product;

        public ParameterSelector(IEnumerable<Parameter> parameters, ProductSelector product, Parameter selectedParameter = null)
        {
            _parameters = parameters;
            _product = product;
            ParametersWithActualFlag = new ObservableCollection<ParameterWithActualFlag>(parameters.Select(x => new ParameterWithActualFlag(x)));
            SelectedParameter = selectedParameter ?? ParametersWithActualFlag.First().Parameter;
        }

        public ObservableCollection<ParameterWithActualFlag> ParametersWithActualFlag { get; }

        private Parameter _selectedParameter;
        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                if(!_parameters.Contains(value)) throw new ArgumentException("Выбранный параметр не из списка.");

                _selectedParameter = value;
                OnPropertyChanged();

                foreach (var parameterWithActualFlag in ParametersWithActualFlag)
                {
                    parameterWithActualFlag.IsActual = !parameterWithActualFlag.Parameter.RequiredPreviousParameters.Any() ||
                                parameterWithActualFlag.Parameter.RequiredPreviousParameters.Any(pp => pp.RequiredParameters.All(x => _product.SelectedParameters.Contains(x)));
                }
                OnPropertyChanged(nameof(IsActual));
            }
        }

        public bool IsActual => ParametersWithActualFlag.Any(x => x.IsActual);
    }
}