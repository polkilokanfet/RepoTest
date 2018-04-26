using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterFlaged : NotifyPropertyChanged
    {
        #region fields
        private bool _isActual;
        private readonly ParameterSelector _parameterSelector;
        private bool _isSelected;
        #endregion

        #region props

        public Parameter Parameter { get; }

        public bool IsActual
        {
            get { return _isActual; }
            set
            {
                if (Equals(_isActual, value)) return;
                _isActual = value;
                OnIsActualChanged();
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



        #endregion

        #region ctor
        public ParameterFlaged(Parameter parameter, ParameterSelector parameterSelector, bool isSelected = false)
        {
            Parameter = parameter;
            _isSelected = isSelected;
            _isActual = isSelected || !Parameter.ParameterRelations.Any();

            _parameterSelector = parameterSelector;
            _parameterSelector.ProductBlockSelector.SelectedParametersChanged += CheckIsActual;
        }
        #endregion

        #region events
        public event Action IsActualChanged;
        public event Action<ParameterFlaged> IsSelectedChanged; 

        protected virtual void OnIsActualChanged()
        {
            IsActualChanged?.Invoke();
            if (!IsActual) IsSelected = false;
        }

        protected virtual void OnIsSelectedChanged(ParameterFlaged parameterFlaged)
        {
            IsSelectedChanged?.Invoke(parameterFlaged);
        }
        #endregion

        public void CheckIsActual()
        {
            IsActual = !Parameter.ParameterRelations.Any() || 
                Parameter.ParameterRelations.Any(x => x.RequiredParameters.AllContainsIn(_parameterSelector.ProductBlockSelector.SelectedProductBlock.Parameters));
        }
    }
}