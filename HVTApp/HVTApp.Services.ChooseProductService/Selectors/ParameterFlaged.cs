using System;
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

        /// <summary>
        /// Флаг актуальности параметра.
        /// </summary>
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

        public ParameterFlaged(Parameter parameter)
        {
            if(parameter == null) throw new ArgumentNullException(nameof(parameter));
            Parameter = parameter;
            _isActual = Parameter.IsOrigin;
        }

        #endregion

        #region events

        public event Action<ParameterFlaged> IsActualChanged;

        #endregion
    }
}