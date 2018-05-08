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

        private List<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterFlaged)
            .Where(x => x != null).Select(x => x.Parameter).ToList();

        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        public ProductBlock SelectedProductBlock
        {
            get
            {
                if (_selectedProductBlock != null && SelectedParameters.AllMembersAreSame(_selectedProductBlock.Parameters))
                    return _selectedProductBlock;
                var result = _existsProductBlocks.SingleOrDefault(x => x.Parameters.AllMembersAreSame(SelectedParameters));
                _selectedProductBlock = result ?? new ProductBlock { Parameters = SelectedParameters };
                return _selectedProductBlock;
            }
            set
            {
                if (SelectedParameters.AllMembersAreSame(value.Parameters)) return;
                var parameterSelectors = ParameterSelectors.ToList();
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlagedChanged -= OnSelectedParameterChanged);
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlaged = null);
                foreach (var parameter in value.Parameters)
                {
                    var selector = ParameterSelectors.Single(ps => ps.ParametersFlaged.Select(x => x.Parameter).Contains(parameter));
                    selector.SelectedParameterFlaged = selector.ParametersFlaged.Single(p => p.Parameter.Equals(parameter));
                }
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

                OnPropertyChanged();
                SelectedProductBlockChanged?.Invoke(this);
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(IEnumerable<Parameter> parameters, List<ProductBlock> existsProductBlocks = null,
            ProductBlock selectedProductBlock = null)
        {
            _existsProductBlocks = existsProductBlocks ?? new List<ProductBlock>();

            //создаем селекторы параметров
            ParameterSelectors = new ObservableCollection<ParameterSelector>(
                GetGroupingParameters(parameters).Select(x => new ParameterSelector(x)));

            //реакция на смену параметра в селекторе
            ParameterSelectors.ToList().ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

            if (selectedProductBlock != null) SelectedProductBlock = selectedProductBlock;
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

        private void OnSelectedParameterChanged(ParameterSelector parameterSelector)
        {
            //перепроверка актуальности параметров
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            foreach (var parameterFlaged in parametersFlaged)
            {
                parameterFlaged.IsActual = parameterFlaged.Parameter.IsOrigin ||
                    parameterFlaged.Parameter.ParameterRelations.
                    Any(x => x.RequiredParameters.AllContainsIn(SelectedProductBlock.Parameters));
            }

            //актуализация выбранных параметров
            foreach (var selector in ParameterSelectors)
            {
                if (selector.SelectedParameterFlaged == null || !selector.SelectedParameterFlaged.IsActual)
                    selector.SelectedParameterFlaged = selector.ParametersFlaged.FirstOrDefault(p => p.IsActual);
            }

            SelectedProductBlockChanged?.Invoke(this);
            OnPropertyChanged(nameof(SelectedProductBlock));
        }
    }
}