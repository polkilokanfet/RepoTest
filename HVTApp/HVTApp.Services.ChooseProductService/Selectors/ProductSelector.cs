using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService;

namespace HVTApp.Services.GetEquipmentService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        private readonly IList<Part> _products;
        private readonly IEnumerable<Parameter> _requiredParameters;
        private Part _selectedPart;

        public ProductSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Part> products, IEnumerable<Parameter> requiredParameters = null, Part preSelectedPart = null)
        {
            _products = products;
            _requiredParameters = requiredParameters == null ? new List<Parameter>() : new List<Parameter>(requiredParameters);

            //������������� ������ �� ������������ �� ������� ������ (���������)
            var parameterses = new List<IEnumerable<Parameter>>(parametersGroups.OrderBy(x => x, new ParametersEnumerableComparer()));

            ParameterSelectors = new ObservableCollection<ParameterSelector>();
            foreach (var parameters in parameterses)
            {
                //��������� ����������� ������ ����������, �������� �� ������������
                //���� � ��������� ���� ������������ �������� - ��������� ������ ���
                var parameterSelector = parameters.Any(_requiredParameters.Contains)
                    ? new ParameterSelector(new[] {parameters.Single(_requiredParameters.Contains)})
                    : new ParameterSelector(parameters);

                ParameterSelectors.Add(parameterSelector);

                //������������� �� ��������� ������ ��������� � ������ ������
                parameterSelector.SelectedParameterChanged += OnSelectedParameterChanged;
                //������������� ������� ������������ ������� ���������
                RefreshParametersActualStatuses(SelectedParameters);
            }

            SelectedPart = GetProduct();

            //���������� �������������� ��������� �������
            if (preSelectedPart != null) SelectedPart = preSelectedPart;
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; set; }

        public IEnumerable<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterWithActualFlag).Where(x => x != null).Select(x => x.Parameter);

        public Part SelectedPart
        {
            get { return _selectedPart; }
            set
            {
                if (Equals(_selectedPart, value)) return;
                var oldValue = _selectedPart;
                _selectedPart = value;

                //��������� � ��������� ���������� ��������� ���������
                var actualParameterSelectors = new List<ParameterSelector>();
                foreach (var parameter in _selectedPart.Parameters)
                {
                    var parameterSelector = ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(parameter));
                    if (!Equals(parameterSelector.SelectedParameterWithActualFlag.Parameter, parameter))
                        parameterSelector.SetSelectedParameterWithActualFlag(parameter);
                    actualParameterSelectors.Add(parameterSelector);
                }
                foreach (var parameterSelector in ParameterSelectors.Except(actualParameterSelectors))
                {
                    if (!Equals(parameterSelector.SelectedParameterWithActualFlag, null)) parameterSelector.SelectedParameterWithActualFlag = null;
                }
                if (!_products.Contains(value)) _products.Add(value);

                OnSelectedProductChanged(oldValue, value);
                OnPropertyChanged();
            }
        }

        public IEnumerable<Parameter> GetRequaredParameters()
        {
            return _requiredParameters;
        }

        private Part GetProduct()
        {
            var result = _products.SingleOrDefault(x => SelectedParameters.AllMembersAreSame(x.Parameters));
            if (result == null)
            {
                result = new Part {Parameters = new List<Parameter>(SelectedParameters)};
                _products.Add(result);
            }
            return result;
        }

        private void RefreshParametersActualStatuses(IEnumerable<Parameter> actualSelectedParameters)
        {
            //������������� ����� ������������ ������� ���������
            foreach (var parameterSelector in ParameterSelectors)
            {
                foreach (var parameterWithActualFlag in parameterSelector.ParametersWithActualFlag)
                    parameterWithActualFlag.IsActual = ParameterIsActual(parameterWithActualFlag.Parameter, actualSelectedParameters);

                var selectedParameterWithActualFlag = parameterSelector.ParametersWithActualFlag.SingleOrDefault(x => Equals(x.Parameter, parameterSelector.SelectedParameterWithActualFlag));

                //���� ��������� �������� ������� ���� ������������, ������� ������ ����������
                if (selectedParameterWithActualFlag != null && !selectedParameterWithActualFlag.IsActual)
                    parameterSelector.SelectedParameterWithActualFlag = parameterSelector.ParametersWithActualFlag.FirstOrDefault(x => x.IsActual);
            }
        }


        private void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
        {
            //���������� ��������� ���������, ������� �������� �� ������� ���������
            List<Parameter> dependendParametersFromOld = new List<Parameter>();
            if (oldParameter != null)
                dependendParametersFromOld.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, oldParameter)));
            //���������� ��������� ���������, ������� ������� �� ������ ���������
            List<Parameter> dependendParametersFromNew = new List<Parameter>();
            if (newParameter != null)
                dependendParametersFromNew.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, newParameter)));

            //�� ���������� ���������
            var notActualSelectedParameters = dependendParametersFromOld.Except(dependendParametersFromNew);
            //���������� ���������
            var actualSelectedParameters = SelectedParameters.Except(notActualSelectedParameters).ToList();
            if (oldParameter != null) actualSelectedParameters.Remove(oldParameter);
            if (newParameter != null) actualSelectedParameters.Add(newParameter);

            //������������� ����� ������������ ������� ���������
            RefreshParametersActualStatuses(actualSelectedParameters);
            OnSelectedParametersChanged();

            SelectedPart = GetProduct();
        }

        private bool ParameterIsActual(Parameter parameter, IEnumerable<Parameter> requiredParameters)
        {
            return !parameter.RequiredPreviousParameters.Any() ||
                    parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(requiredParameters));
        }

        //����� �� ��������� ��� ������������ ����� ������� ���������
        private bool ParameterDependsFrom(Parameter targetParameter, Parameter possibleNeededParameter)
        {
            if (!targetParameter.RequiredPreviousParameters.Any()) return false;
            return targetParameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.Contains(possibleNeededParameter));
        }





        public event Action<Part, Part> SelectedProductChanged;

        protected virtual void OnSelectedProductChanged(Part oldPart, Part newPart)
        {
            SelectedProductChanged?.Invoke(oldPart, newPart);
        }

        private event Action SelectedParametersChanged;

        protected virtual void OnSelectedParametersChanged()
        {
            SelectedParametersChanged?.Invoke();
        }
    }
}