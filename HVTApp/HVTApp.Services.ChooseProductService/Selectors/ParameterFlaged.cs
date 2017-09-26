using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterFlaged : NotifyPropertyChanged
    {
        private bool _isActual = true;

        public ParameterFlaged(Parameter parameter)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
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

        public void RefreshActualStatus(IEnumerable<Parameter> requiredParameters)
        {
            IsActual = !Parameter.RequiredPreviousParameters.Any() ||
                        Parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(requiredParameters));
        }


        public event Action<ParameterFlaged> IsActualChanged;

        protected virtual void OnIsActualChanged(ParameterFlaged obj)
        {
            IsActualChanged?.Invoke(obj);
        }

        public override string ToString()
        {
            return Parameter.ToString();
        }
    }
}