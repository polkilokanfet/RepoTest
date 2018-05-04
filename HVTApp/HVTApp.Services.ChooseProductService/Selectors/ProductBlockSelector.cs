using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductBlockSelector : NotifyPropertyChanged
    {
        #region fields

        private ProductBlock _selectedProductBlock;
        private List<ProductBlock> _productBlocks;

        #endregion

        #region private props

        private List<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterFlaged)
            .Where(x => x != null).Select(x => x.Parameter).ToList();

        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        public ProductBlock SelectedProductBlock
        {
            get
            {
                var result = _productBlocks.SingleOrDefault(x => x.Parameters.AllMembersAreSame(SelectedParameters));
                return result ?? new ProductBlock { Parameters = SelectedParameters };
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(IEnumerable<Parameter> parameters, List<ProductBlock> productBlocks = null,
            ProductBlock selectedProductBlock = null)
        {
            //������� ��������� ����������
            ParameterSelectors = new ObservableCollection<ParameterSelector>(
                GetGroupingParameters(parameters).Select(x => new ParameterSelector(x, this)));

            //������� �� ����� ��������� � ���������
            _productBlocks = productBlocks ?? new List<ProductBlock>();
            foreach (var parameterSelector in ParameterSelectors)
            {
                parameterSelector.SelectedParameterFlagedChanged += (ps) =>
                {
                    SelectedProductBlockChanged?.Invoke(this);
                    OnPropertyChanged(nameof(SelectedProductBlock));
                };
            }
        }

        #endregion

        #region events

        public event Action<ProductBlockSelector> SelectedProductBlockChanged;

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