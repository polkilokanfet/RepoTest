using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyIsActualChanged
    {
        public ReadOnlyObservableCollection<ParameterFlaged> ParametersFlaged { get; }

        public ParameterFlaged SelectedParameterFlaged
        {
            get { return ParametersFlaged.SingleOrDefault(x => x.IsSelected); }
            set
            {
                if (value != null && !ParametersFlaged.Contains(value)) throw new ArgumentException("Выбран параметр не из списка.");

                if (Equals(SelectedParameterFlaged, value)) return;

                var oldValue = SelectedParameterFlaged;
                if (oldValue != null) oldValue.IsSelected = false;
                if (value != null) value.IsSelected = true;

                OnSelectedParameterChanged(oldValue, value);
                OnPropertyChanged();

                RefreshActualStatus();
            }
        }

        public Parameter SelectedParameter
        {
            get { return SelectedParameterFlaged?.Parameter; }
            set
            {
                if (value == null)
                {
                    SelectedParameterFlaged = null;
                    return;
                }

                if (value != null && !ParametersFlaged.Select(x => x.Parameter).Contains(value))
                    throw new ArgumentException("Выбран параметр не из списка.");

                SelectedParameterFlaged = ParametersFlaged.Single(x => Equals(value, x.Parameter));
            }
        }


        public ParameterSelector(IEnumerable<Parameter> parameters, Parameter preSelectedParameter = null)
        {
            //проверка входных данных
            if (parameters == null) throw new ArgumentNullException(nameof(parameters), @"Вы не передали параметры");
            var parametersList = new List<Parameter>(parameters.OrderBy(x => x.Value));
            if (!parametersList.Any()) throw new ArgumentException(@"Вы передали пустой список параметров", nameof(parameters));
            if (!parametersList.All(x => Equals(x.GroupId, parametersList.First().GroupId))) throw new ArgumentException(@"Параметры должны быть из одной группы", nameof(parameters));

            ParametersFlaged = new ReadOnlyObservableCollection<ParameterFlaged>(
                new ObservableCollection<ParameterFlaged>(parametersList.Select(x => new ParameterFlaged(x))));
            //подписываемся на смену актуальности параметра
            ParametersFlaged.ToList().ForEach(x => x.IsActualChanged += ParameterFlagedOnIsActualChanged);

            //предварительно выбранный параметр
            if (preSelectedParameter != null)
                SelectedParameter = preSelectedParameter;
            else
                SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(x => x.IsActual);
        }

        private void ParameterFlagedOnIsActualChanged(object sender)
        {
            var parameterFlaged = (ParameterFlaged) sender;

            //меняем выбранный параметр, если он теперь не актуален
            if (SelectedParameterFlaged == null ||
                (Equals(SelectedParameterFlaged, parameterFlaged) && !parameterFlaged.IsActual))
                SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(x => x.IsActual);

            RefreshActualStatus();
        }

        /// <summary>
        /// обновлем флаги актуальности каждого параметра
        /// </summary>
        /// <param name="requiredParameters"></param>
        //public void RefreshParametersActualStatuses(IEnumerable<Parameter> requiredParameters)
        //{
        //    ParametersFlaged.ToList().ForEach(x => x.RefreshActualStatus(requiredParameters));
        //}


        /// <summary>
        /// Обновление статуса актуальности группы
        /// </summary>
        public void RefreshActualStatus()
        {
            IsActual = this.ParametersFlaged.Any(x => x.IsActual);
        }

        public bool Contains(Parameter parameter)
        {
            return ParametersFlaged.Select(x => x.Parameter).Contains(parameter);
        }

        public event Action<ParameterFlaged, ParameterFlaged> SelectedParameterChanged;
        protected virtual void OnSelectedParameterChanged(ParameterFlaged oldParameter, ParameterFlaged newParameter)
        {
            SelectedParameterChanged?.Invoke(oldParameter, newParameter);
        }

    }

}