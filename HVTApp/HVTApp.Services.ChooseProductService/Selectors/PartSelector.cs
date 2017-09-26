using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class PartSelector : NotifyPropertyChanged
    {
        private readonly IList<Part> _parts;
        private readonly IEnumerable<Parameter> _requiredParameters;
        private Part _selectedPart;

        public PartSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Part> parts, IEnumerable<Parameter> requiredParameters = null, Part preSelectedPart = null)
        {
            _parts = parts;
            _requiredParameters = requiredParameters == null ? new List<Parameter>() : new List<Parameter>(requiredParameters);

            //упорядочиваем группы по отдаленности от опорной группы (параметра)
            var parameterses = new List<IEnumerable<Parameter>>(parametersGroups.OrderBy(x => x, new ParametersEnumerableComparer()));

            ParameterSelectors = new ObservableCollection<ParameterSelector>();
            foreach (var parameters in parameterses)
            {
                //исключаем возможность выбора параметров, отличных от обязательных
                //если в селекторе есть обязательный параметр - оставляем только его
                var parameterSelector = parameters.Any(_requiredParameters.Contains)
                    ? new ParameterSelector(new[] { parameters.Single(_requiredParameters.Contains) })
                    : new ParameterSelector(parameters);

                ParameterSelectors.Add(parameterSelector);

                //подписываемся на изменение выбора параметра в каждой группе
                parameterSelector.SelectedParameterChanged += OnSelectedParameterChanged;
                //перепроверяем статусы актуальности каждого параметра
                RefreshParametersActualStatuses(SelectedParameters);
            }

            SelectedPart = GetPart();

            //назаначаем предварительно выбранный продукт
            if (preSelectedPart != null) SelectedPart = preSelectedPart;
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        public IEnumerable<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterFlaged).Where(x => x != null).Select(x => x.Parameter);

        public Part SelectedPart
        {
            get { return _selectedPart; }
            set
            {
                if (Equals(_selectedPart, value)) return;
                var oldValue = _selectedPart;
                _selectedPart = value;

                //назначаем в селекторы актуальные выбранные параметры
                var actualParameterSelectors = new List<ParameterSelector>();
                foreach (var parameter in _selectedPart.Parameters)
                {
                    var parameterSelector = ParameterSelectors.Single(x => x.ParametersFlaged.Select(p => p.Parameter).Contains(parameter));
                    parameterSelector.SelectedParameter = parameter;
                    actualParameterSelectors.Add(parameterSelector);
                }
                foreach (var parameterSelector in ParameterSelectors.Except(actualParameterSelectors))
                {
                    if (!Equals(parameterSelector.SelectedParameterFlaged, null)) parameterSelector.SelectedParameterFlaged = null;
                }
                if (!_parts.Contains(value)) _parts.Add(value);

                OnSelectedProductChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        public IEnumerable<Parameter> GetRequaredParameters()
        {
            return _requiredParameters;
        }

        private Part GetPart()
        {
            var result = _parts.SingleOrDefault(x => SelectedParameters.AllMembersAreSame(x.Parameters));
            if (result == null)
            {
                result = new Part { Parameters = new List<Parameter>(SelectedParameters) };
                _parts.Add(result);
            }
            return result;
        }

        private void RefreshParametersActualStatuses(IEnumerable<Parameter> actualSelectedParameters)
        {
            //перепроверяем флаги актуальности каждого параметра
            foreach (var parameterSelector in ParameterSelectors)
            {
                foreach (var parameterFlaged in parameterSelector.ParametersFlaged)
                    parameterFlaged.RefreshActualStatus(actualSelectedParameters);

                //если выбранный параметр потерял свою актуальность, выбирам другой актуальный
                var selectedParameterFlaged = parameterSelector.ParametersFlaged.SingleOrDefault(x => Equals(x.Parameter, parameterSelector.SelectedParameterFlaged?.Parameter));
                if (selectedParameterFlaged != null && !selectedParameterFlaged.IsActual)
                    parameterSelector.SelectedParameterFlaged = parameterSelector.ParametersFlaged.FirstOrDefault(x => x.IsActual);
            }
        }


        private void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            //определяем выбранные параметры, которые зависели от старого параметра
            List<Parameter> dependendParametersFromOld = new List<Parameter>();
            if (oldParameter != null)
                dependendParametersFromOld.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, oldParameter)));
            //определяем выбранные параметры, которые зависят от нового параметра
            List<Parameter> dependendParametersFromNew = new List<Parameter>();
            if (newParameter != null)
                dependendParametersFromNew.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, newParameter)));

            //не актуальные параметры
            var notActualSelectedParameters = dependendParametersFromOld.Except(dependendParametersFromNew);
            //актуальные параметры
            var actualSelectedParameters = SelectedParameters.Except(notActualSelectedParameters).ToList();
            if (oldParameter != null) actualSelectedParameters.Remove(oldParameter);
            if (newParameter != null) actualSelectedParameters.Add(newParameter);

            //перепроверяем флаги актуальности каждого параметра
            RefreshParametersActualStatuses(actualSelectedParameters);
            OnSelectedParametersChanged();

            SelectedPart = GetPart();
        }

        //нужен ли параметру для актуальности выбор другого параметра
        private bool ParameterDependsFrom(Parameter targetParameter, Parameter possibleNeededParameter)
        {
            if (!targetParameter.RequiredPreviousParameters.Any()) return false;
            return targetParameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.Contains(possibleNeededParameter));
        }





        public event Action<Part, Part> SelectedPartChanged;

        protected virtual void OnSelectedProductChanged(Part oldPart, Part newPart)
        {
            SelectedPartChanged?.Invoke(oldPart, newPart);
        }

        private event Action SelectedParametersChanged;

        protected virtual void OnSelectedParametersChanged()
        {
            SelectedParametersChanged?.Invoke();
        }
    }
}