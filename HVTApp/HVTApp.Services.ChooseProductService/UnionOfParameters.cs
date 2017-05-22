using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using HVTApp.Model.Wrappers;

namespace HVTApp.Services.ChooseProductService
{
    /// <summary>
    /// Объединение параметров
    /// </summary>
    public class UnionOfParameters : INotifyPropertyChanged
    {
        /// <summary>
        /// Группа параметров объединеия.
        /// </summary>
        public ParameterGroupWrapper Group => Parameters.First().Group;

        /// <summary>
        /// Все параметры объединения.
        /// </summary>
        public IList<ParameterWrapper> Parameters { get; }

        /// <summary>
        /// Параметры для выбора (актуальные в данном контексте).
        /// </summary>
        public ObservableCollection<ParameterWrapper> ParametersToSelect { get; } = new ObservableCollection<ParameterWrapper>();

        private ParameterWrapper _selectedParameter;
        /// <summary>
        /// Выбранный в объединении параметр
        /// </summary>
        public ParameterWrapper SelectedParameter
        {
            get
            {
                if (!IsActual) return null;
                if (ParametersToSelect.Contains(_selectedParameter)) return _selectedParameter;
                SelectedParameter = ParametersToSelect.First();
                return ParametersToSelect.First();
            }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                if (value != null && !ParametersToSelect.Contains(value))
                    throw new ArgumentException("Выбранный параметр не находится в списке параметров, подходящих для выбора.");

                if (value != null) _selectedParameter = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsActual));
                SelectedParameterChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// флаг актуальности объединения параметров (стоит ли его показывать).
        /// </summary>
        public bool IsActual => ParametersToSelect.Any();

        public UnionOfParameters(IEnumerable<ParameterWrapper> parameters)
        {
            var p = parameters.ToList();
            if (p.Select(x => x.Group).Distinct().Count() != 1)
                throw new ArgumentException("Параметры из разных групп.");

            Parameters = new List<ParameterWrapper>(p);

            RefreshParametersToSelect(new List<ParameterWrapper>());
        }

        /// <summary>
        /// Обновление параметров, которые могут быть выбраны в текущем контексте.
        /// </summary>
        /// <param name="selectedParameters"></param>
        public void RefreshParametersToSelect(IEnumerable<ParameterWrapper> selectedParameters)
        {
            var actualParamsToSelect = Parameters.Where(x => x.CanBeSelected(selectedParameters)).ToList();

            if (actualParamsToSelect.Except(ParametersToSelect).Any())
            {
                ParametersToSelect.Clear();
                actualParamsToSelect.ForEach(ParametersToSelect.Add);

                OnPropertyChanged(nameof(IsActual));
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }


        public event EventHandler SelectedParameterChanged;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}