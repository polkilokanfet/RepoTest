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
        public ParameterGroupWrapper Group { get; }

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
                return ParametersToSelect.First();
            }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                if (value != null) _selectedParameter = value;

                OnPropertyChanged();
                OnPropertyChanged(nameof(IsActual));
                UnionChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// флаг актуальности объединения параметров (стоит ли его показывать).
        /// </summary>
        public bool IsActual => ParametersToSelect.Any();

        public UnionOfParameters(IEnumerable<ParameterWrapper> parameters)
        {
            var p = parameters.ToList();

            Group = p.First().Group;
            Parameters = new List<ParameterWrapper>(p);
        }

        /// <summary>
        /// Обновление параметров, которые могут быть выбраны в текущем контексте.
        /// </summary>
        /// <param name="selectedParameters"></param>
        public void RefreshParametersToSelect(IEnumerable<ParameterWrapper> selectedParameters)
        {
            var refreshedParamsToSelect = Parameters.Where(x => x.CanBeSelected(selectedParameters)).ToList();

            //var paramsToAdd = refreshedParamsToSelect.Except(ParametersToSelect).ToList(); //добавить к параметрам для выбора
            //var paramsToRemove = ParametersToSelect.Except(refreshedParamsToSelect).ToList(); //удалить из параметров для выбора

            if (refreshedParamsToSelect.Except(ParametersToSelect).Any())
                //if (paramsToAdd.Any() || paramsToRemove.Any())
            {
                ParametersToSelect.Clear();
                refreshedParamsToSelect.ForEach(ParametersToSelect.Add);

                //    foreach (var param in paramsToAdd) ParametersToSelect.Add(param); //добавляем
                //foreach (var param in paramsToRemove) ParametersToSelect.Remove(param); //удаляем

                UnionChanged?.Invoke(this, EventArgs.Empty);
                OnPropertyChanged(nameof(IsActual));
                OnPropertyChanged(nameof(SelectedParameter));
            }
        }


        public event EventHandler UnionChanged; 

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}