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
       
        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        public ProductBlock SelectedProductBlock
        {
            get { return _selectedProductBlock; }
            set
            {
                if (Equals(_selectedProductBlock, value)) return;
                _selectedProductBlock = value;
                SelectedProductBlockChanged?.Invoke(this);
                OnPropertyChanged();
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(AllProductParameters allProductParameters, IEnumerable<Parameter> parameters, 
            IEnumerable<Parameter> selectedParameters2 = null)
        {
            var selectedParams = selectedParameters2 ?? new List<Parameter>();

            //создаем селекторы параметров
            ParameterSelectors = new ObservableCollection<ParameterSelector>(GetGroupingParameters(parameters).
                Select(x => new ParameterSelector(x, this, x.SingleOrDefault(selectedParams.Contains))));

            //реакция на смену параметра в селекторе
            foreach (var parameterSelector in ParameterSelectors)
            {
                parameterSelector.SelectedParameterFlagedChanged += (ps) =>
                {
                    var selectedParameters = ParameterSelectors.Select(x => x.SelectedParameterFlaged)
                        .Where(x => x != null).Select(x => x.Parameter).ToList();
                    if (SelectedProductBlock.Parameters.AllMembersAreSame(selectedParameters)) return;

                    var result = allProductParameters.ProductBlocks
                        .SingleOrDefault(x => x.Parameters.AllMembersAreSame(selectedParameters));
                    SelectedProductBlock = result ?? new ProductBlock {Parameters = selectedParameters};
                };
            }
        }

        #endregion

        #region events

        public event Action<ProductBlockSelector> SelectedProductBlockChanged;

        #endregion


        //группировка параметрам по группе и упорядочивание их
        private IEnumerable<IEnumerable<Parameter>> GetGroupingParameters(IEnumerable<Parameter> parameters)
        {
            var groups = parameters.GroupBy(x => x.ParameterGroup).OrderBy(x => x, new ParametersEnumerableComparer());
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.Value);
            }
        }

        //выбор первого базового параметра
        public void SelectFirstParameter()
        {
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            var selectedParameterFlaged = parametersFlaged.First(x => !x.Parameter.ParameterRelations.Any());
            var parameterSelector = ParameterSelectors.Single(x => x.ParametersFlaged.Contains(selectedParameterFlaged));
            parameterSelector.SelectedParameterFlaged = selectedParameterFlaged;
        }
    }
}