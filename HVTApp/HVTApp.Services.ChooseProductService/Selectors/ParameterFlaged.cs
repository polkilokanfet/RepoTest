using System;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterFlaged : NotifyPropertyChanged
    {
        #region fields

        private bool _isActual;

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
                IsActualChanged?.Invoke(this);
                OnPropertyChanged();
            }
        }

        #endregion

        #region ctor

        public ParameterFlaged(Parameter parameter, ProductBlockSelector productBlockSelector)
        {
            Parameter = parameter;
            _isActual = Parameter.IsOrigin;

            productBlockSelector.SelectedProductBlockChanged += pbs =>
            {
                IsActual = Parameter.IsOrigin || 
                    Parameter.ParameterRelations.Any(x => x.RequiredParameters.AllContainsIn(pbs.SelectedProductBlock.Parameters));
            };
        }

        #endregion

        #region events

        public event Action<ParameterFlaged> IsActualChanged;

        #endregion
    }
}