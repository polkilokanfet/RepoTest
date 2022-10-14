using System;
using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterFlaged : NotifyPropertyChanged, IComparable<ParameterFlaged>
    {
        #region props

        public Parameter Parameter { get; }

        private bool _isActual;
        /// <summary>
        /// Флаг актуальности параметра.
        /// </summary>
        public bool IsActual
        {
            get => _isActual;
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
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            _isActual = Parameter.IsOrigin;
        }

        #endregion

        #region events

        public event Action<ParameterFlaged> IsActualChanged;

        #endregion

        public override string ToString()
        {
            return this.IsActual 
                ? $"{this.Parameter} - актуален" 
                : $"{this.Parameter} - не актуален";
        }

        public int CompareTo(ParameterFlaged other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return this.Parameter.CompareTo(other.Parameter);
        }
    }
}