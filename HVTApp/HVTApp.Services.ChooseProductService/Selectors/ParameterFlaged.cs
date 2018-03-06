using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterFlaged : NotifyPropertyChanged
    {
        private bool _isActual;
        private bool _isSelected;

        public Parameter Parameter { get; }

        public ParameterFlaged(Parameter parameter, ParameterSelector parameterSelector, bool isSelected = false)
        {
            Parameter = parameter;
            _isSelected = isSelected;
            _isActual = isSelected || !Parameter.ParameterRelations.Any();

            parameterSelector.ProductBlockSelector.SelectedParametersChanged += CheckIsActual;
        }

        public bool IsActual
        {
            get { return _isActual; }
            set
            {
                if (Equals(_isActual, value)) return;
                _isActual = value;
                OnIsActualChanged(this);
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if (Equals(_isSelected, value)) return;
                _isSelected = value;
                OnIsSelectedChanged(this);
                OnPropertyChanged();
            }
        }

        public void CheckIsActual(IEnumerable<Parameter> parameters)
        {
            IsActual = Parameter.IsActual(parameters);
        }

        #region events
        public event Action<ParameterFlaged> IsActualChanged;
        public event Action<ParameterFlaged> IsSelectedChanged; 

        protected virtual void OnIsActualChanged(ParameterFlaged parameterFlaged)
        {
            IsActualChanged?.Invoke(parameterFlaged);
            if (!IsActual)
                IsSelected = false;
        }

        protected virtual void OnIsSelectedChanged(ParameterFlaged parameterFlaged)
        {
            IsSelectedChanged?.Invoke(parameterFlaged);
        }
        #endregion
    }
}