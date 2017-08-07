using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ParameterSelector : NotifyPropertyChanged
    {
        private Parameter _selectedParameter;
        private bool _isActual = true;

        public ParameterSelector(IEnumerable<Parameter> parameters, Parameter selectedParameter = null)
        {
            ParametersWithActualFlag = new ObservableCollection<ParameterWithActualFlag>();
            foreach (var parameter in parameters)
            {
                var parameterWithActualFlag = new ParameterWithActualFlag(parameter);
                ParametersWithActualFlag.Add(parameterWithActualFlag);
                //подписываемся на смену актуальности параметра
                parameterWithActualFlag.IsActualChanged += ParameterWithActualFlagOnIsActualChanged; 
            }

            SelectedParameter = selectedParameter ?? ParametersWithActualFlag.FirstOrDefault(x => x.IsActual)?.Parameter;
        }

        private void ParameterWithActualFlagOnIsActualChanged(ParameterWithActualFlag parameterWithActualFlag)
        {
            //меняем выбранный параметр, если он теперь не актуален
            if (Equals(SelectedParameter, parameterWithActualFlag.Parameter) && !parameterWithActualFlag.IsActual)
                SelectedParameter = ParametersWithActualFlag.FirstOrDefault(x => x.IsActual)?.Parameter;

            //актуальна ли теперь вся группа?
            IsActual = this.ParametersWithActualFlag.Any(x => x.IsActual);
        }


        public ObservableCollection<ParameterWithActualFlag> ParametersWithActualFlag { get; }


        public Parameter SelectedParameter
        {
            get { return _selectedParameter; }
            set
            {
                if (Equals(_selectedParameter, value)) return;

                if(value != null && !ParametersWithActualFlag.Select(x => x.Parameter).Contains(value))
                    throw new ArgumentException("Выбран параметр не из списка.");

                var oldValue = _selectedParameter;
                _selectedParameter = value;

                OnSelectedParameterChanged(oldValue, value);
                OnPropertyChanged();

                OnPropertyChanged(nameof(IsActual));
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