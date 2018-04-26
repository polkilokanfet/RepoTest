using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        #region fields
        private ParameterFlaged _selectedParameterFlaged;
        #endregion

        #region props

        public ProductBlockSelector ProductBlockSelector { get; }

        public bool IsActual => ParametersFlaged.Any(x => x.IsActual);
        public string GroupName => ParametersFlaged.First().Parameter.ParameterGroup.Name;

        public ParameterFlaged SelectedParameterFlaged
        {
            get { return _selectedParameterFlaged; }
            set
            {
                if (Equals(_selectedParameterFlaged, value)) return;
                _selectedParameterFlaged = value;
                value.IsSelected = true;
                OnPropertyChanged();
                SelectedParameterFlagedChanged?.Invoke();
            }
        }

        public ObservableCollection<ParameterFlaged> ParametersFlaged { get; }
        #endregion

        #region ctor
        public ParameterSelector(IEnumerable<Parameter> parameters, ProductBlockSelector productBlockSelector, 
            Parameter selectedParameter = null)
        {
            if(productBlockSelector == null) throw new ArgumentNullException(nameof(productBlockSelector));
            ProductBlockSelector = productBlockSelector;

            var parametersFlaged = parameters.Select(x => new ParameterFlaged(x, this, Equals(x, selectedParameter)));
            ParametersFlaged = new ObservableCollection<ParameterFlaged>(parametersFlaged);

            if (selectedParameter != null)
                SelectedParameterFlaged = ParametersFlaged.Single(x => Equals(x.Parameter, selectedParameter));

            //реакция на изменение актуальности параметра
            foreach (var parameterFlaged in ParametersFlaged)
                parameterFlaged.IsActualChanged += ParameterFlagedOnIsActualChanged;
        }
        #endregion

        #region events
        public event Action SelectedParameterFlagedChanged;
        #endregion

        //реакция на изменение актуальности параметра
        private void ParameterFlagedOnIsActualChanged()
        {
            if(SelectedParameterFlaged == null || !SelectedParameterFlaged.IsActual)
                SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(x => x.IsActual);
            OnPropertyChanged(nameof(IsActual));
        }

    }
}
