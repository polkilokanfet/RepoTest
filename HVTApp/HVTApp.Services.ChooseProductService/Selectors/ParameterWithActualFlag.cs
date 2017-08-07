using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterWithActualFlag : NotifyPropertyChanged
    {
        private bool _isActual = true;

        public ParameterWithActualFlag(Parameter parameter)
        {
            Parameter = parameter;
        }

        public Parameter Parameter { get; }

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



        public event Action<ParameterWithActualFlag> IsActualChanged;

        protected virtual void OnIsActualChanged(ParameterWithActualFlag obj)
        {
            IsActualChanged?.Invoke(obj);
        }
    }
}