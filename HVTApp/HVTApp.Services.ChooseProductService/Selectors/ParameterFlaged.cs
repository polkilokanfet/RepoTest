using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterFlaged : NotifyIsActualChanged
    {
        private bool _isSelected;

        public ParameterFlaged(Parameter parameter, ParameterSelector parameterSelector)
        {
            if (parameter == null) throw new ArgumentNullException(nameof(parameter));
            
            Parameter = parameter;
            IsSelected = false;
        }

        public Parameter Parameter { get; }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                if(Equals(_isSelected, value)) return;
                _isSelected = value;
                OnIsSelectedChanged();
            }
        }

        public event Action IsSelectedChanged;

        public void RefreshActualStatus(IEnumerable<Parameter> selectedParameters)
        {
            IsActual = !Parameter.RequiredPreviousParameters.Any() ||
                        Parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(selectedParameters));
        }

        public override string ToString()
        {
            return Parameter.ToString();
        }

        protected virtual void OnIsSelectedChanged()
        {
            IsSelectedChanged?.Invoke();
        }
    }
}