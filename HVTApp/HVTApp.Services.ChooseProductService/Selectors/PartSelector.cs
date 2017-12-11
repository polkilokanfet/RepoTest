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

            //������������� ������ �� ������������ �� ������� ������ (���������)
            var parameterGroups = new List<IEnumerable<Parameter>>(parametersGroups.OrderBy(x => x, new ParametersEnumerableComparer()));

            ParameterSelectors = new ObservableCollection<ParameterSelector>();
            foreach (var parameters in parameterGroups)
            {
                //��������� ����������� ������ ����������, �������� �� ������������
                //���� � ��������� ���� ������������ �������� - ��������� ������ ���
                var parameterSelector = parameters.Any(_requiredParameters.Contains)
                    ? new ParameterSelector(parameters.Intersect(_requiredParameters))
                    : new ParameterSelector(parameters);

                ParameterSelectors.Add(parameterSelector);

                //������������� �� ��������� ������ ��������� � ������ ������
                parameterSelector.SelectedParameterChanged += OnSelectedParameterChanged;
            }
            //�������� ������� ������������ ������� ���������
            RefreshParametersActualStatuses(SelectedParameters);

            //���������� �������������� ��������� �������
            if (preSelectedPart != null) SelectedPart = preSelectedPart;
            else RefreshSelectedPart();
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

                //��������� � ��������� ���������� ��������� ���������
                if (!SelectedParameters.AllMembersAreSame(_selectedPart.Parameters))
                    SetParametersInSelectors(_selectedPart.Parameters);

                if (!_parts.Contains(value)) _parts.Add(value);

                OnSelectedProductChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        private void SetParametersInSelectors(IEnumerable<Parameter> parameters)
        {
                var parameterSelectors = new List<ParameterSelector>(ParameterSelectors);
                foreach (var parameter in parameters)
                {
                    var parameterSelector = ParameterSelectors.Single(x => x.Contains(parameter));
                    if (!Equals(parameterSelector.SelectedParameter, parameter)) parameterSelector.SelectedParameter = parameter;
                    parameterSelectors.Remove(parameterSelector);
                }

                parameterSelectors.ForEach(x => x.SelectedParameter = null);
        }

        private void RefreshSelectedPart()
        {
            var result = _parts.SingleOrDefault(x => SelectedParameters.AllMembersAreSame(x.Parameters));
            if (result == null)
            {
                result = new Part { Parameters = new List<Parameter>(SelectedParameters) };
                _parts.Add(result);
            }
            SelectedPart = result;
        }

        private void RefreshParametersActualStatuses(IEnumerable<Parameter> actualSelectedParameters)
        {
            ParameterSelectors.ToList().ForEach(x => x.RefreshParametersActualStatuses(actualSelectedParameters));
        }

        private void OnSelectedParameterChanged(ParameterFlaged oldParameter, ParameterFlaged newParameter)
        {
            //���������� ��������� ���������, ������� �������� �� ������� ���������
            var dependendParametersFromOld = GetDependendParameters(oldParameter?.Parameter);

            //���������� ��������� ���������, ������� ������� �� ������ ���������
            var dependendParametersFromNew = GetDependendParameters(newParameter?.Parameter);

            //�� ���������� ���������
            var notActualSelectedParameters = dependendParametersFromOld.Except(dependendParametersFromNew);
            //���������� ���������
            var actualSelectedParameters = SelectedParameters.Except(notActualSelectedParameters).ToList();
            if (oldParameter != null) actualSelectedParameters.Remove(oldParameter?.Parameter);
            if (newParameter != null) actualSelectedParameters.Add(newParameter?.Parameter);

            //������������� ����� ������������ ������� ���������
            RefreshParametersActualStatuses(actualSelectedParameters);

            RefreshSelectedPart();
        }

        private IEnumerable<Parameter> GetDependendParameters(Parameter parameter)
        {
            return parameter != null
                ? new List<Parameter>(SelectedParameters.Where(x => ParameterDependsFrom(x, parameter)))
                : new List<Parameter>();
        }

        //����� �� ��������� ��� ������������ ����� ������� ���������
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

    }
}