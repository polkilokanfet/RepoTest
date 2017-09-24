using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class PartSelector : NotifyPropertyChanged
    {
        private IEnumerable<Parameter> SelectedParameters
        {
            get { return ParameterSelectors.Select(x => x.SelectedParameter); }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                //���������� ��� ��������� ���������� �� ������� �� ����������
                foreach (var parameterSelector in ParameterSelectors)
                {
                    parameterSelector.SelectedParameterChanged -= ParameterSelectorOnSelectedParameterChanged;
                    parameterSelector.SelectedParameter = null;
                }

                foreach (var parameter in value)
                    ParameterSelectors.Single(x => x.Parameters.Contains(parameter)).SelectedParameter = parameter;

                ParameterSelectors.ToList().ForEach(x => x.SelectedParameterChanged += ParameterSelectorOnSelectedParameterChanged);

                OnSelectedPartChanged();
                OnPropertyChanged(nameof(SelectedPart));
            }
        }
        
        public ObservableCollection<ParameterSelector> ParameterSelectors { get; } = new ObservableCollection<ParameterSelector>();

        public Part SelectedPart
        {
            get { return new Part {Parameters = new List<Parameter>(SelectedParameters)}; }
            set
            {
                if (value == null) throw new ArgumentNullException();
                SelectedParameters = value.Parameters;
            }
        }

        public PartSelector(IEnumerable<Parameter> parameters, Part preSelectedPart = null)
        {
            foreach (var parametersGrouped in parameters.GroupBy(x => x.Group))
                ParameterSelectors.Add(new ParameterSelector(this, parametersGrouped));

            ParameterSelectors.ToList().ForEach(x => x.SelectedParameterChanged += ParameterSelectorOnSelectedParameterChanged);

            if (preSelectedPart != null) SelectedPart = preSelectedPart;
        }


        private void ParameterSelectorOnSelectedParameterChanged()
        {
            OnSelectedPartChanged();
            OnPropertyChanged(nameof(SelectedPart));
        }

        public event Action SelectedPartChanged;

        protected virtual void OnSelectedPartChanged()
        {
            SelectedPartChanged?.Invoke();
        }
    }

    //public class PartSelector : NotifyPropertyChanged
    //{
    //    private readonly IList<Part> _products;
    //    private readonly IEnumerable<Parameter> _requiredParameters;
    //    private Part _selectedPart;

    //    public PartSelector(IEnumerable<IEnumerable<Parameter>> parametersGroups, IList<Part> products, IEnumerable<Parameter> requiredParameters = null, Part preSelectedPart = null)
    //    {
    //        _products = products;
    //        _requiredParameters = requiredParameters == null ? new List<Parameter>() : new List<Parameter>(requiredParameters);

    //        //������������� ������ �� ������������ �� ������� ������ (���������)
    //        var parameterses = new List<IEnumerable<Parameter>>(parametersGroups.OrderBy(x => x, new ParametersEnumerableComparer()));

    //        ParameterSelectors = new ObservableCollection<ParameterSelector>();
    //        foreach (var parameters in parameterses)
    //        {
    //            //��������� ����������� ������ ����������, �������� �� ������������
    //            //���� � ��������� ���� ������������ �������� - ��������� ������ ���
    //            var parameterSelector = parameters.Any(_requiredParameters.Contains)
    //                ? new ParameterSelector(new[] {parameters.Single(_requiredParameters.Contains)})
    //                : new ParameterSelector(parameters);

    //            ParameterSelectors.Add(parameterSelector);

    //            //������������� �� ��������� ������ ��������� � ������ ������
    //            parameterSelector.SelectedParameterChanged += OnSelectedParameterChanged;
    //            //������������� ������� ������������ ������� ���������
    //            RefreshParametersActualStatuses(SelectedParameters);
    //        }

    //        SelectedPart = GetPart();

    //        //���������� �������������� ��������� �������
    //        if (preSelectedPart != null) SelectedPart = preSelectedPart;
    //    }

    //    public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

    //    public IEnumerable<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterWithActualFlag).Where(x => x != null).Select(x => x.Parameter);

    //    public Part SelectedPart
    //    {
    //        get { return _selectedPart; }
    //        set
    //        {
    //            if (Equals(_selectedPart, value)) return;
    //            var oldValue = _selectedPart;
    //            _selectedPart = value;

    //            //��������� � ��������� ���������� ��������� ���������
    //            var actualParameterSelectors = new List<ParameterSelector>();
    //            foreach (var parameter in _selectedPart.Parameters)
    //            {
    //                var parameterSelector = ParameterSelectors.Single(x => x.ParametersWithActualFlag.Select(p => p.Parameter).Contains(parameter));
    //                if (!Equals(parameterSelector.SelectedParameterWithActualFlag.Parameter, parameter))
    //                    parameterSelector.SetSelectedParameterWithActualFlag(parameter);
    //                actualParameterSelectors.Add(parameterSelector);
    //            }
    //            foreach (var parameterSelector in ParameterSelectors.Except(actualParameterSelectors))
    //            {
    //                if (!Equals(parameterSelector.SelectedParameterWithActualFlag, null)) parameterSelector.SelectedParameterWithActualFlag = null;
    //            }
    //            if (!_products.Contains(value)) _products.Add(value);

    //            OnSelectedProductChanged(oldValue, value);
    //            OnPropertyChanged();
    //        }
    //    }

    //    public IEnumerable<Parameter> GetRequaredParameters()
    //    {
    //        return _requiredParameters;
    //    }

    //    private Part GetPart()
    //    {
    //        var result = _products.SingleOrDefault(x => SelectedParameters.AllMembersAreSame(x.Parameters));
    //        if (result == null)
    //        {
    //            result = new Part {Parameters = new List<Parameter>(SelectedParameters)};
    //            _products.Add(result);
    //        }
    //        return result;
    //    }

    //    private void RefreshParametersActualStatuses(IEnumerable<Parameter> actualSelectedParameters)
    //    {
    //        //������������� ����� ������������ ������� ���������
    //        foreach (var parameterSelector in ParameterSelectors)
    //        {
    //            foreach (var parameterWithActualFlag in parameterSelector.ParametersWithActualFlag)
    //                parameterWithActualFlag.IsActual = ParameterIsActual(parameterWithActualFlag.Parameter, actualSelectedParameters);

    //            var selectedParameterWithActualFlag = parameterSelector.ParametersWithActualFlag.SingleOrDefault(x => Equals(x.Parameter, parameterSelector.SelectedParameterWithActualFlag?.Parameter));

    //            //���� ��������� �������� ������� ���� ������������, ������� ������ ����������
    //            if (selectedParameterWithActualFlag != null && !selectedParameterWithActualFlag.IsActual)
    //                parameterSelector.SelectedParameterWithActualFlag = parameterSelector.ParametersWithActualFlag.FirstOrDefault(x => x.IsActual);
    //        }
    //    }


    //    private void OnSelectedParameterChanged(Parameter oldParameter, Parameter newParameter)
    //    {
    //        //���������� ��������� ���������, ������� �������� �� ������� ���������
    //        List<Parameter> dependendParametersFromOld = new List<Parameter>();
    //        if (oldParameter != null)
    //            dependendParametersFromOld.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, oldParameter)));
    //        //���������� ��������� ���������, ������� ������� �� ������ ���������
    //        List<Parameter> dependendParametersFromNew = new List<Parameter>();
    //        if (newParameter != null)
    //            dependendParametersFromNew.AddRange(SelectedParameters.Where(x => ParameterDependsFrom(x, newParameter)));

    //        //�� ���������� ���������
    //        var notActualSelectedParameters = dependendParametersFromOld.Except(dependendParametersFromNew);
    //        //���������� ���������
    //        var actualSelectedParameters = SelectedParameters.Except(notActualSelectedParameters).ToList();
    //        if (oldParameter != null) actualSelectedParameters.Remove(oldParameter);
    //        if (newParameter != null) actualSelectedParameters.Add(newParameter);

    //        //������������� ����� ������������ ������� ���������
    //        RefreshParametersActualStatuses(actualSelectedParameters);
    //        OnSelectedParametersChanged();

    //        SelectedPart = GetPart();
    //    }

    //    private bool ParameterIsActual(Parameter parameter, IEnumerable<Parameter> requiredParameters)
    //    {
    //        return !parameter.RequiredPreviousParameters.Any() ||
    //                parameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.AllContainsIn(requiredParameters));
    //    }

    //    //����� �� ��������� ��� ������������ ����� ������� ���������
    //    private bool ParameterDependsFrom(Parameter targetParameter, Parameter possibleNeededParameter)
    //    {
    //        if (!targetParameter.RequiredPreviousParameters.Any()) return false;
    //        return targetParameter.RequiredPreviousParameters.Any(x => x.RequiredParameters.Contains(possibleNeededParameter));
    //    }





    //    public event Action<Part, Part> SelectedPartChanged;

    //    protected virtual void OnSelectedProductChanged(Part oldPart, Part newPart)
    //    {
    //        SelectedPartChanged?.Invoke(oldPart, newPart);
    //    }

    //    private event Action SelectedParametersChanged;

    //    protected virtual void OnSelectedParametersChanged()
    //    {
    //        SelectedParametersChanged?.Invoke();
    //    }
    //}
}