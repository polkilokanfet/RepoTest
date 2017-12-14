using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : INotifyPropertyChanged
    {
        private readonly IEnumerable<Parameter> _parameters;
        private Parameter _selectedParameter;

        public ParameterSelector(IEnumerable<Parameter> parameters)
        {
            _parameters = parameters;
            Parameters = new ObservableCollection<Parameter>(_parameters);
        }

        public ObservableCollection<Parameter> Parameters { get; }

        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;
                var oldValue = _selectedParameter;
                _selectedParameter = value;
                OnSelectedParameterChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        public event Action<Parameter, Parameter> SelectedParameterChanged;

        protected virtual void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            SelectedParameterChanged?.Invoke(oldParameter, newParameter);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class ProductSelector
    {
        private readonly List<ParameterSelector> parameterSelectors = new List<ParameterSelector>();
        public ProductSelector(IEnumerable<Parameter> parameters)
        {
            var parametersGrouped = parameters.GroupBy(x => x.GroupId);
            foreach (var group in parametersGrouped)
            {
                parameterSelectors.Add(new ParameterSelector(group));
            }
        }
    }
}
