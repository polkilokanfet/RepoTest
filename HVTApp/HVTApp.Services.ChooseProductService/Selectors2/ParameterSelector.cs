using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.DataAccess.Annotations;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : INotifyPropertyChanged
    {
        private readonly IEnumerable<Parameter> _parameters;
        private Parameter _selectedParameter;

        public ParameterSelector(IEnumerable<Parameter> parameters, ProductSelector productSelector)
        {
            _parameters = parameters;
            Parameters = new ObservableCollection<Parameter>();

            productSelector.SelectedParametersChanged += ProductSelectorOnSelectedParametersChanged;

            ProductSelectorOnSelectedParametersChanged(productSelector.SelectedParameters);
        }

        private void ProductSelectorOnSelectedParametersChanged(IEnumerable<Parameter> selectedParameters)
        {
            var actualParameters = _parameters.Where(x => x.IsActual(selectedParameters)).ToList();

            if (Parameters.AllMembersAreSame(actualParameters)) return;

            Parameters.Clear();
            actualParameters.ForEach(Parameters.Add);

            OnIsActualChanged();

            if (SelectedParameter == null || !Parameters.Contains(SelectedParameter))
                SelectedParameter = Parameters.FirstOrDefault();
        }

        public ObservableCollection<Parameter> Parameters { get; }

        public bool IsActual => Parameters.Any();

        public event Action<ParameterSelector> IsActualChanged;

        private bool _isActualBefore = false;
        protected virtual void OnIsActualChanged()
        {
            if (_isActualBefore == IsActual) return;

            _isActualBefore = IsActual;

            IsActualChanged?.Invoke(this);
        }

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
}
