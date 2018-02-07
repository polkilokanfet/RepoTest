using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductBlockSelector : NotifyPropertyChanged
    {
        public static IEnumerable<ProductBlock> ProductBlocks { get; set; } = new List<ProductBlock>();

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        public ProductBlockSelector(IEnumerable<Parameter> parameters, Product selectedProduct)
        {
            //������� ��������� ����������
            var parameterSelectors =
                GetGroupingParameters(parameters).Select(x => new ParameterSelector(x, this, Cross(x, selectedProduct)));
            ParameterSelectors = new ObservableCollection<ParameterSelector>(parameterSelectors);

            //������� �� ����� ��������� � ���������
            foreach (var parameterSelector in ParameterSelectors)
                parameterSelector.SelectedParameterFlagedChanged += parSel =>
                {
                    OnSelectedParametersChanged(SelectedParameters);
                    OnPropertyChanged(nameof(SelectedProductBlock));
                    OnPropertyChanged(nameof(SelectedParameters));
                };
        }



        public IEnumerable<ParameterFlaged> SelectedParametersFlaged => ParameterSelectors.Select(x => x.SelectedParameterFlaged).Where(x => x != null);

        public IEnumerable<Parameter> SelectedParameters => SelectedParametersFlaged.Select(x => x.Parameter);

        public ProductBlock SelectedProductBlock => ProductBlocks.FirstOrDefault(x => x.Parameters.AllMembersAreSame(SelectedParameters)); 

        //����������� ���������� �� ������ � �������������� ��
        private IEnumerable<IEnumerable<Parameter>> GetGroupingParameters(IEnumerable<Parameter> parameters)
        {
            var groups = parameters.GroupBy(x => x.ParameterGroup);
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.Value);
            }
        }

        private Parameter Cross(IEnumerable<Parameter> parameters, Product product)
        {
            return product == null ? null : parameters.SingleOrDefault(x => product.ProductBlock.Parameters.Contains(x));
        }


        //����� ������� �������� ���������
        public void SelectFirstParameter()
        {
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            var selectedParameterFlaged = parametersFlaged.First(x => !x.Parameter.ParameterRelations.Any());
            var parameterSelector = ParameterSelectors.Single(x => x.ParametersFlaged.Contains(selectedParameterFlaged));
            parameterSelector.SelectedParameterFlaged = selectedParameterFlaged;
        }

        #region events
        public event Action<IEnumerable<Parameter>> SelectedParametersChanged;

        protected virtual void OnSelectedParametersChanged(IEnumerable<Parameter> parameters)
        {
            SelectedParametersChanged?.Invoke(parameters);
        }
        #endregion
    }

    public static class Ext
    {
        /// <summary>
        /// �������� �� ������ ����������, ������� � ����� ������ � ������������� �����������.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Parameter> RemoveUseLess(this IEnumerable<Parameter> targetParameters, IEnumerable<Parameter> requiredParameters)
        {
            var result = targetParameters.ToList();
            foreach (var requiredParameter in requiredParameters)
            {
                var toExcept = result.Where(x => Equals(x.ParameterGroupId, requiredParameter.ParameterGroup.Id))
                        .Except(new List<Parameter> {requiredParameter});
                result = result.Except(toExcept).ToList();
            }
            return result;
        }
    }
}