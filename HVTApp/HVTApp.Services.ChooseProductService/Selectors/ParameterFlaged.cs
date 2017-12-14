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

        public ParameterFlaged(Parameter parameter)
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

        public void SubscribingToChangeActualStatus(PartSelector partSelector)
        {
            partSelector.SelectedParametersChanged += RefreshActualStatus;
        }

        private void RefreshActualStatus(IEnumerable<Parameter> selectedParameters)
        {
            IsActual = !Parameter.RequiredPreviousParameters.Any() ||
                        Parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(selectedParameters));
            if (!IsActual)
                IsSelected = false;
        }

        protected virtual void OnIsSelectedChanged()
        {
            IsSelectedChanged?.Invoke();
        }

        public override string ToString()
        {
            return Parameter.ToString();
        }
    }
}