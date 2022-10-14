using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;
using HVTApp.Services.GetProductService.Comparers;

namespace HVTApp.Services.GetProductService
{
    public class ProductBlockSelector : NotifyPropertyChanged
    {

        #region fields

        private readonly Bank _bank;

        #endregion

        #region private props

        private List<Parameter> SelectedParameters => ParameterSelectors
            .Select(x => x.SelectedParameterFlaged)
            .Where(x => x != null)
            .Select(x => x.Parameter)
            .ToList();

        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        /// <summary>
        /// ��������� ����
        /// </summary>
        public ProductBlock SelectedBlock
        {
            get => _bank.GetBlock(SelectedParameters);
            set
            {
                var blockToSet = value;
                if(blockToSet == null) throw new ArgumentNullException(nameof(blockToSet));

                //���� ��������� ��������� ��������� � ��������� ������ �����
                if (SelectedParameters.MembersAreSame(blockToSet.Parameters)) return;

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

                OnPropertyChanged();
                SelectedBlockChanged?.Invoke(this);
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(IEnumerable<Parameter> parameters, Bank bank, ProductBlock selectedProductBlock = null)
        {
            _bank = bank;

            var parametersArray = parameters as Parameter[] ?? parameters.ToArray();

            //������� ��������� ����������
            ParameterSelectors = new ObservableCollection<ParameterSelector>(GetParameterSelectors(parametersArray));

            //�������� �� ����� ��������� � ���������
            ParameterSelectors.ToList().ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

            //���� ���� ��������� ����
            if (selectedProductBlock != null)
            {
                if(selectedProductBlock.Parameters.AllContainsIn(parametersArray) == false)
                    throw new ArgumentException("��������� ����� �� ������������� ��������� ����������.");

                SelectedBlock = selectedProductBlock;
            }
        }

        #endregion

        #region events

        /// <summary>
        /// ������� ��������� ���������� �����.
        /// </summary>
        public event Action<ProductBlockSelector> SelectedBlockChanged;

        #endregion

        /// <summary>
        /// ����������� ���������� � �������������� ��
        /// </summary>
        /// <param name="parameters">���������</param>
        /// <returns></returns>
        private IEnumerable<ParameterSelector> GetParameterSelectors(IEnumerable<Parameter> parameters)
        {
            //����������� ���������� �� ������� � �������������� ��.
            var groups = parameters
                .GroupBy(parameter => parameter.ParameterGroup.Id)
                //.OrderBy(x => x, new ParametersEnumerableComparerByPaths())
                .Select(x => new ParameterSelector(x))
                .OrderBy(x => x)
                .ToList();

            foreach (var group in groups)
            {
                yield return group;
            }
        }

        /// <summary>
        /// ������� �� ��������� ���������� ��������� � ���������.
        /// </summary>
        /// <param name="parameterSelector"></param>
        private void OnSelectedParameterChanged(ParameterSelector parameterSelector)
        {
            //������������ ������������ ����������
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            foreach (var parameter in parametersFlaged)
            {
                parameter.IsActual = parameter.Parameter.IsOrigin ||
                                     parameter.Parameter.ParameterRelations.Any(x => x.RequiredParameters.AllContainsIn(SelectedParameters));
            }

            //������� ����� �����
            SelectedBlockChanged?.Invoke(this);
            OnPropertyChanged(nameof(SelectedBlock));
        }
    }
}