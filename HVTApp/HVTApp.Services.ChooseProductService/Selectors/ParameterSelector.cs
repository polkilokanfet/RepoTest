using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged, IComparable<ParameterSelector>
    {
        #region props

        public bool IsActual => ParametersFlaged.Any(x => x.IsActual) && ParametersFlaged.Count(x => x.IsActual) > 1;

        private ParameterFlaged _selectedParameterFlaged;
        public ParameterFlaged SelectedParameterFlaged
        {
            get => _selectedParameterFlaged;
            set
            {
                if (Equals(_selectedParameterFlaged, value)) return;
                _selectedParameterFlaged = value;
                SelectedParameterFlagedChanged?.Invoke(this);
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ParameterFlaged> ParametersFlaged { get; }

        #endregion

        #region ctor

        public ParameterSelector(IEnumerable<Parameter> parameters)
        {
            var parametersArray = parameters as Parameter[] ?? parameters.ToArray();

            if (parametersArray == null) throw new ArgumentNullException(nameof(parameters));
            if (parametersArray.Any() == false) throw new ArgumentException(nameof(parameters), "В селектор пришло пустое перечисление параметров");
            if (parametersArray.GroupBy(x => x.ParameterGroup.Id).Count() > 1) throw new ArgumentException(nameof(parameters), "В селектор пришли параметры из разных групп");

            //упорядочивание параметров
            var parametersFlaged = parametersArray.Select(x => new ParameterFlaged(x));
            ParametersFlaged = new ObservableCollection<ParameterFlaged>(parametersFlaged.OrderBy(x => x));

            //реакция на изменение актуальности параметра
            foreach (var parameterFlaged in ParametersFlaged)
            {
                parameterFlaged.IsActualChanged += parameter =>
                {
                    //актуализация выбранного параметра
                    if (SelectedParameterFlaged == null || !SelectedParameterFlaged.IsActual)
                        SelectedParameterFlaged = ParametersFlaged.FirstOrDefault(p => p.IsActual);
                    //проверка актуальности селектора
                    OnPropertyChanged(nameof(IsActual));
                };
            }
        }

        #endregion

        #region events

        public event Action<ParameterSelector> SelectedParameterFlagedChanged;

        #endregion

        public override string ToString()
        {
            return $"{ParametersFlaged.First().Parameter.ParameterGroup.Name} ({this.ParametersFlaged.ToStringEnum()})";
        }

        public int CompareTo(ParameterSelector other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;

            var parametersThis = this.ParametersFlaged.Select(x => x.Parameter).ToList();
            var parametersOther = other.ParametersFlaged.Select(x => x.Parameter).ToList();

            //if (parametersThis.Select(x => x.Value).Any(x => x == "Трансформатор"))
            //{
            //    if (parametersOther.Select(x => x.Value).Any(x => x == "Напряжения"))
            //    {

            //    }
            //}

            //if (parametersOther.Select(x => x.Value).Any(x => x == "Трансформатор"))
            //{
            //    if (parametersThis.Select(x => x.Value).Any(x => x == "Напряжения"))
            //    {

            //    }
            //}

            //if (parametersThis.Select(x => x.Value).Any(x => x == "Напряжения"))
            //{
            //    if (parametersOther.Select(x => x.Value).Any(x => x == "Трансформатор"))
            //    {

            //    }
            //}

            //if (parametersOther.Select(x => x.Value).Any(x => x == "Напряжения"))
            //{
            //    if (parametersThis.Select(x => x.Value).Any(x => x == "Трансформатор"))
            //    {

            //    }
            //}

            foreach (var parameterThis in parametersThis)
            {
                foreach (var parameterOther in parametersOther)
                {
                    var result = parameterThis.CompareToPaths(parameterOther);
                    if (result != 0)
                        return result;
                }
            }

            return parametersThis.First().CompareTo(parametersOther.First());
        }
    }
}
