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

        private ProductBlock _selectedProductBlock;
        private readonly List<ProductBlock> _existsProductBlocks;

        #endregion

        #region private props

        private List<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterFlaged).Where(x => x != null).Select(x => x.Parameter).ToList();

        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        /// <summary>
        /// ��������� ����
        /// </summary>
        public ProductBlock SelectedProductBlock
        {
            get
            {
                //���� ��� ��������� ��������� ���������
                if (_selectedProductBlock != null && SelectedParameters.AllMembersAreSame(_selectedProductBlock.Parameters))
                    return _selectedProductBlock;

                //����� � ������������ ������
                var result = _existsProductBlocks.SingleOrDefault(x => x.Parameters.AllMembersAreSame(SelectedParameters));
                if (result != null)
                {
                    _selectedProductBlock = result;
                    return result;
                }

                //�������� ������ �����
                _selectedProductBlock = new ProductBlock { Parameters = SelectedParameters };
                _selectedProductBlock.Name = _selectedProductBlock.ParametersToString();
                _selectedProductBlock.StructureCostNumber = "blank";
                _existsProductBlocks.Add(_selectedProductBlock);
                return _selectedProductBlock;
            }
            set
            {
                var blockToSet = value;
                if(blockToSet == null) throw new ArgumentNullException(nameof(blockToSet));

                //���� ��������� ��������� ��������� � ��������� ������ �����
                if (SelectedParameters.AllMembersAreSame(blockToSet.Parameters)) return;


                var parameterSelectors = ParameterSelectors.ToList();
                //������������ �� ������� ������ ������ ���������
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlagedChanged -= OnSelectedParameterChanged);
                //�������� ��������� ���������
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlaged = null);

                //���������� � ������ �������� ������������ ���������
                foreach (var parameter in blockToSet.Parameters)
                {
                    //����� ���������
                    var selector = ParameterSelectors.Single(ps => ps.ParametersFlaged.Select(x => x.Parameter).Contains(parameter));
                    //����� ���������
                    selector.SelectedParameterFlaged = selector.ParametersFlaged.Single(p => p.Parameter.Equals(parameter));
                    selector.SelectedParameterFlaged.IsActual = true;
                }

                //������������� �� ������� ������ ������ ��������� � ������ ���������
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

                OnSelectedParameterChanged(null);

                _selectedProductBlock = blockToSet;

                OnPropertyChanged();
                SelectedProductBlockChanged?.Invoke(this);
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(IEnumerable<Parameter> parameters, List<ProductBlock> existsProductBlocks,
            ProductBlock selectedProductBlock = null)
        {
            _existsProductBlocks = existsProductBlocks;

            //������� ��������� ����������
            var groupedParameters = GetGroupingParameters(parameters).Select(x => new ParameterSelector(x));
            ParameterSelectors = new ObservableCollection<ParameterSelector>(groupedParameters);

            //�������� �� ����� ��������� � ���������
            ParameterSelectors.ToList().ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

            //���� ���� ��������� ����
            if (selectedProductBlock != null)
            {
                if(!selectedProductBlock.Parameters.AllContainsIn(parameters))
                    throw new ArgumentException("��������� ����� �� ������������� ��������� ����������.");
                SelectedProductBlock = selectedProductBlock;
            }
        }

        #endregion

        #region events

        /// <summary>
        /// ������� ��������� ���������� �����.
        /// </summary>
        public event Action<ProductBlockSelector> SelectedProductBlockChanged;

        #endregion

        /// <summary>
        /// ����������� ���������� �� ������ � �������������� ��
        /// </summary>
        /// <param name="parameters">���������</param>
        /// <returns></returns>
        private IEnumerable<IEnumerable<Parameter>> GetGroupingParameters(IEnumerable<Parameter> parameters)
        {
            //����������� ���������� �� ������� � �������������� ��.
            var groups = parameters.GroupBy(x => x.ParameterGroup.Id).
                                    OrderBy(x => x, new ParametersEnumerableComparer());
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.Value);
            }
        }

        /// <summary>
        /// ����� ������� �������� ���������.
        /// </summary>
        public void SelectFirstParameter()
        {
            //��� ���������
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);

            //��������, � �������� ��� ������������ ����������
            var selectedParameterFlaged = parametersFlaged.First(x => !x.Parameter.ParameterRelations.Any());

            //����� ���������, ����������� ����� �������� � ���������� ���������
            var parameterSelector = ParameterSelectors.Single(x => x.ParametersFlaged.Contains(selectedParameterFlaged));
            parameterSelector.SelectedParameterFlaged = selectedParameterFlaged;
        }

        /// <summary>
        /// ������� �� ��������� ���������� ��������� � ���������.
        /// </summary>
        /// <param name="parameterSelector"></param>
        private void OnSelectedParameterChanged(ParameterSelector parameterSelector)
        {
            //������������ ������������ ����������
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            foreach (var parameterFlaged in parametersFlaged)
            {
                parameterFlaged.IsActual = parameterFlaged.Parameter.IsOrigin ||
                    parameterFlaged.Parameter.ParameterRelations.
                    Any(x => x.RequiredParameters.AllContainsIn(SelectedParameters));
            }

            SelectedProductBlockChanged?.Invoke(this);
            OnPropertyChanged(nameof(SelectedProductBlock));
        }
    }
}