using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        /// <summary>
        /// Родительский селектор
        /// </summary>
        public ProductSelector ProductSelector { get; }
        public bool IsActual => ParametersFlaged.Any(x => x.IsActual);

        private ParameterFlaged _selectedParameterFlaged;
        public ParameterFlaged SelectedParameterFlaged
        {
            get { return _selectedParameterFlaged; }
            set
            {
                if (Equals(_selectedParameterFlaged, value)) return;
                _selectedParameterFlaged = value;
                OnPropertyChanged();
                OnSelectedParameterFlagedChanged(this);
            }
        }

        public ObservableCollection<ParameterFlaged> ParametersFlaged { get; }

        public ParameterSelector(IEnumerable<Parameter> parameters, ProductSelector productSelector, Parameter selectedParameter = null)
        {
            ProductSelector = productSelector;

            var parametersFlaged = parameters.Select(x => new ParameterFlaged(x, this, Equals(x, selectedParameter)));
            ParametersFlaged = new ObservableCollection<ParameterFlaged>(parametersFlaged);

            if (selectedParameter != null)
                SelectedParameterFlaged = ParametersFlaged.Single(x => Equals(x.Parameter, selectedParameter));

            //реакция на изменение актуальности параметра
            foreach (var parameterFlaged in ParametersFlaged)
                parameterFlaged.IsActualChanged += ParameterFlagedOnIsActualChanged;
        }

        //реакция на изменение актуальности параметра
        private void ParameterFlagedOnIsActualChanged(ParameterFlaged parameterFlaged)
        {
            if (parameterFlaged.IsActual)
            {
                if (SelectedParameterFlaged == null || !SelectedParameterFlaged.IsActual)
                    SelectedParameterFlaged = ParametersFlaged.First(x => x.IsActual);
            }
            else
            {
                if (SelectedParameterFlaged != null && !SelectedParameterFlaged.IsActual)
                    SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(x => x.IsActual);
            }

            OnPropertyChanged(nameof(IsActual));
        }


        #region events
        public event Action<ParameterSelector> SelectedParameterFlagedChanged;

        protected virtual void OnSelectedParameterFlagedChanged(ParameterSelector parameterSelector)
        {
            SelectedParameterFlagedChanged?.Invoke(parameterSelector);
        }
        #endregion
    }
}
