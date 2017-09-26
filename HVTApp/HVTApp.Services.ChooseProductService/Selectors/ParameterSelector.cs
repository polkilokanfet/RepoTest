using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        private ParameterFlaged _selectedParameterFlaged;
        private bool _isActual = true;

        public ParameterGroup Group => ParametersFlaged.First().Parameter.Group;
        public ReadOnlyObservableCollection<ParameterFlaged> ParametersFlaged { get; }

        public ParameterFlaged SelectedParameterFlaged
        {
            get { return _selectedParameterFlaged; }
            set
            {
                if (Equals(_selectedParameterFlaged, value)) return;

                if (value != null && !ParametersFlaged.Contains(value)) throw new ArgumentException("Выбран параметр не из списка.");
                //if (value != null && !value.IsActual) throw new ArgumentException("Параметр не актуален");

                var oldValue = _selectedParameterFlaged;
                _selectedParameterFlaged = value;

                OnSelectedParameterChanged(oldValue?.Parameter, value?.Parameter);
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsActual));
            }
        }

        public Parameter SelectedParameter
        {
            get { return SelectedParameterFlaged?.Parameter; }
            set
            {
                if (value != null && !ParametersFlaged.Select(x => x.Parameter).Contains(value))
                    throw new ArgumentException("Выбран параметр не из списка.");

                SelectedParameterFlaged = ParametersFlaged.Single(x => Equals(value, x.Parameter));
            }
        }

        public bool IsActual
        {
            get { return _isActual; }
            set
            {
                if (Equals(_isActual, value)) return;
                _isActual = value;
                OnIsActualChanged();
                OnPropertyChanged();
            }
        }

        public ParameterSelector(IEnumerable<Parameter> parameters, Parameter preSelectedParameter = null)
        {
            //проверка входных данных
            if (parameters == null) throw new ArgumentNullException(nameof(parameters), @"Вы не передали параметры");
            var parametersList = new List<Parameter>(parameters.OrderBy(x => x.Value));
            if (!parametersList.Any()) throw new ArgumentException(@"Вы передали пустой список параметров", nameof(parameters));
            if (!parametersList.All(x => Equals(x.Group, parametersList.First().Group))) throw new ArgumentException(@"Параметры должны быть из одной группы", nameof(parameters));

            ParametersFlaged = new ReadOnlyObservableCollection<ParameterFlaged>(
                new ObservableCollection<ParameterFlaged>(parametersList.Select(x => new ParameterFlaged(x))));
            //подписываемся на смену актуальности параметра
            ParametersFlaged.ToList().ForEach(x => x.IsActualChanged += ParameterFlagedOnIsActualChanged);

            SelectedParameterFlaged = preSelectedParameter != null
                ? ParametersFlaged.FirstOrDefault(x => x.IsActual && Equals(preSelectedParameter, x.Parameter))
                : ParametersFlaged.FirstOrDefault(x => x.IsActual);
        }

        private void ParameterFlagedOnIsActualChanged(ParameterFlaged parameterFlaged)
        {
            //меняем выбранный параметр, если он теперь не актуален
            if (SelectedParameterFlaged == null ||
                (Equals(SelectedParameterFlaged, parameterFlaged) && !parameterFlaged.IsActual))
                SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(x => x.IsActual);

            RefreshActualStatus();
        }

        /// <summary>
        /// Обновление статуса актуальности группы
        /// </summary>
        public void RefreshActualStatus()
        {
            IsActual = this.ParametersFlaged.Any(x => x.IsActual);
        }

        #region Events
        public event Action<Parameter, Parameter> SelectedParameterChanged;
        protected virtual void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            SelectedParameterChanged?.Invoke(oldParameter, newParameter);
        }

        public event Action IsActualChanged;
        protected virtual void OnIsActualChanged()
        {
            IsActualChanged?.Invoke();
        }
        #endregion

    }

}