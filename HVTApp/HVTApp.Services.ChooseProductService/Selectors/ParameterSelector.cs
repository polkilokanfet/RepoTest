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

        public bool IsActual => ParametersFlaged.Any(x => x.IsActual);
        public string GroupName => ParametersFlaged.First().Parameter.ParameterGroup.Name;

        public ParameterFlaged SelectedParameterFlaged
        {
            get { return _selectedParameterFlaged; }
            set
            {
                if (Equals(_selectedParameterFlaged, value)) return;
                _selectedParameterFlaged = value;
                OnPropertyChanged();
                SelectedParameterFlagedChanged?.Invoke(this);
            }
        }

        public ObservableCollection<ParameterFlaged> ParametersFlaged { get; }

        #endregion

        #region ctor

        public ParameterSelector(IEnumerable<Parameter> parameters, ProductBlockSelector productBlockSelector, 
            Parameter selectedParameter = null)
        {
            if(productBlockSelector == null) throw new ArgumentNullException(nameof(productBlockSelector));

            ParametersFlaged = new ObservableCollection<ParameterFlaged>(
                parameters.Select(x => new ParameterFlaged(x, productBlockSelector)));

            if (selectedParameter != null)
                SelectedParameterFlaged = ParametersFlaged.Single(x => Equals(x.Parameter, selectedParameter));

            //реакция на изменение актуальности параметра
            foreach (var parameterFlaged in ParametersFlaged)
            {
                parameterFlaged.IsActualChanged += pf =>
                {
                    if (SelectedParameterFlaged == null || !SelectedParameterFlaged.IsActual)
                        SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(x => x.IsActual);

                    OnPropertyChanged(nameof(IsActual));
                };
            }
        }

        #endregion

        #region events

        public event Action<ParameterSelector> SelectedParameterFlagedChanged;

        #endregion
    }
}
