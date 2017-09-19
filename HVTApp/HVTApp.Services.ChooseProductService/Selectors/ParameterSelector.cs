using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        private ParameterWithActualFlag _selectedParameterWithActualFlag;
        private bool _isActual = true;

        public ParameterSelector(IEnumerable<Parameter> parameters, Parameter preSelectedParameter = null)
        {
            if (parameters == null) throw new ArgumentNullException(nameof(parameters), @"Вы не передали параметры");

            var parametersList = new List<Parameter>(parameters.OrderBy(x =>x.Value));
            if (!parametersList.Any()) throw new ArgumentException(@"Вы передали пустой список параметров", nameof(parameters));
            if (!parametersList.All(x => Equals(x.Group, parametersList.First().Group))) throw new ArgumentException(@"Параметры должны быть из одной группы", nameof(parameters));



            ParametersWithActualFlag = new ObservableCollection<ParameterWithActualFlag>();
            foreach (var parameter in parametersList)
            {
                var parameterWithActualFlag = new ParameterWithActualFlag(parameter);
                ParametersWithActualFlag.Add(parameterWithActualFlag);
                //подписываемся на смену актуальности параметра
                parameterWithActualFlag.IsActualChanged += ParameterWithActualFlagOnIsActualChanged; 
            }

            SelectedParameterWithActualFlag = preSelectedParameter != null
                ? ParametersWithActualFlag.FirstOrDefault(x => x.IsActual && Equals(preSelectedParameter, x.Parameter))
                : ParametersWithActualFlag.FirstOrDefault(x => x.IsActual);
        }

        private void ParameterWithActualFlagOnIsActualChanged(ParameterWithActualFlag parameterWithActualFlag)
        {
            //меняем выбранный параметр, если он теперь не актуален
            if (SelectedParameterWithActualFlag == null || 
                (Equals(SelectedParameterWithActualFlag, parameterWithActualFlag) && !parameterWithActualFlag.IsActual))
                SelectedParameterWithActualFlag = ParametersWithActualFlag.FirstOrDefault(x => x.IsActual);

            //актуальна ли теперь вся группа?
            IsActual = this.ParametersWithActualFlag.Any(x => x.IsActual);
        }

        public ParameterGroup Group => ParametersWithActualFlag.First().Parameter.Group;
        public ObservableCollection<ParameterWithActualFlag> ParametersWithActualFlag { get; }

        public ParameterWithActualFlag SelectedParameterWithActualFlag
        {
            get { return _selectedParameterWithActualFlag; }
            set
            {
                if (Equals(_selectedParameterWithActualFlag, value)) return;

                if (value != null && !ParametersWithActualFlag.Contains(value))
                    throw new ArgumentException("Выбран параметр не из списка.");

                if (value != null && !value.IsActual)
                    throw new ArgumentException("Параметр не актуален");

                var oldValue = _selectedParameterWithActualFlag;
                _selectedParameterWithActualFlag = value;

                OnSelectedParameterChanged(oldValue?.Parameter, value?.Parameter);
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsActual));
            }
        }

        public void SetSelectedParameterWithActualFlag(Parameter parameter)
        {
            if (parameter != null && !ParametersWithActualFlag.Select(x => x.Parameter).Contains(parameter))
                throw new ArgumentException("Выбран параметр не из списка.");

            SelectedParameterWithActualFlag = ParametersWithActualFlag.SingleOrDefault(x => Equals(parameter, x.Parameter));
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

    }
}