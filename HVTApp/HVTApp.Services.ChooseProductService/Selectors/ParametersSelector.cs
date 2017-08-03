using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParametersSelector : NotifyPropertyChanged
    {

        public ParametersSelector(IEnumerable<Parameter> parameters, Parameter selectedParameter, ProductSelector product)
        {
            ParameterSelectors = new ObservableCollection<ParameterSelector>(parameters.Select(x => new ParameterSelector(x, product.SelectedParameters)));
            SelectedParameter = selectedParameter ?? ParameterSelectors.First().Parameter;
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        private Parameter _selectedParameter;
        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;
                _selectedParameter = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(IsActual));
            }
        }

        public bool IsActual => ParameterSelectors.Any(x => x.CanBeSelected);
    }
}