using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductBlockSelector : NotifyPropertyChanged
    {
        #region fields
        private readonly AllProductParameters _allProductParameters;
        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }
        public ProductBlock SelectedProductBlock
        {
            get
            {
                var selectedParameters = ParameterSelectors.Select(x => x.SelectedParameterFlaged)
                    .Where(x => x != null).Select(x => x.Parameter).ToList();
                var result = _allProductParameters.ProductBlocks
                    .SingleOrDefault(x => x.Parameters.AllMembersAreSame(selectedParameters));
                return result ?? new ProductBlock { Parameters = selectedParameters };
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(AllProductParameters allProductParameters, IEnumerable<Parameter> parameters, 
            IEnumerable<Parameter> selectedParameters = null)
        {
            _allProductParameters = allProductParameters;
            var selectedParams = selectedParameters ?? new List<Parameter>();

            //������� ��������� ����������
            var parameterSelectors = GetGroupingParameters(parameters).
                Select(x => new ParameterSelector(x, this, x.SingleOrDefault(selectedParams.Contains)));
            ParameterSelectors = new ObservableCollection<ParameterSelector>(parameterSelectors);

            //������� �� ����� ��������� � ���������
            foreach (var parameterSelector in ParameterSelectors)
            {
                parameterSelector.SelectedParameterFlagedChanged += () =>
                {
                    OnSelectedParametersChanged();
                    OnPropertyChanged(nameof(SelectedProductBlock));
                };
            }

            //���������� ��������� ��������� (������ ��� ��������, � ��� ���)
            if (selectedParams.Any())
                OnSelectedParametersChanged();
        }

        #endregion

        #region events
        public event Action SelectedParametersChanged;

        protected void OnSelectedParametersChanged()
        {
            SelectedParametersChanged?.Invoke();
        }
        #endregion


        //����������� ���������� �� ������ � �������������� ��
        private IEnumerable<IEnumerable<Parameter>> GetGroupingParameters(IEnumerable<Parameter> parameters)
        {
            var groups = parameters.GroupBy(x => x.ParameterGroup).OrderBy(x => x, new ParametersEnumerableComparer());
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.Value);
            }
        }

        //����� ������� �������� ���������
        public void SelectFirstParameter()
        {
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            var selectedParameterFlaged = parametersFlaged.First(x => !x.Parameter.ParameterRelations.Any());
            var parameterSelector = ParameterSelectors.Single(x => x.ParametersFlaged.Contains(selectedParameterFlaged));
            parameterSelector.SelectedParameterFlaged = selectedParameterFlaged;
        }
    }
}