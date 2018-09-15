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

        private readonly Bank _bank;
        private ProductBlock _selectedBlock;

        #endregion

        #region private props

        private List<Parameter> SelectedParameters => ParameterSelectors.Select(x => x.SelectedParameterFlaged).Where(x => x != null).Select(x => x.Parameter).ToList();

        #endregion

        #region props

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }

        /// <summary>
        /// Выбранный блок
        /// </summary>
        public ProductBlock SelectedBlock
        {
            get
            {
                //если все выбранные параметры совпадают
                if (_selectedBlock != null && SelectedParameters.MembersAreSame(_selectedBlock.Parameters))
                    return _selectedBlock;

                //создание нового блока
                _selectedBlock = new ProductBlock { Parameters = SelectedParameters };

                //поиск в существующих блоках
                var exist = _bank.Blocks.SingleOrDefault(x => x.Equals(_selectedBlock));
                if (exist != null)
                {
                    _selectedBlock = exist;
                    return _selectedBlock;
                }

                _selectedBlock.Designation = _bank.Designator.GetDesignation(_selectedBlock);
                _selectedBlock.StructureCostNumber = "blank";

                //добавление блока в банк
                _bank.Blocks.Add(_selectedBlock);
                return _selectedBlock;
            }
            set
            {
                var blockToSet = value;
                if(blockToSet == null) throw new ArgumentNullException(nameof(blockToSet));

                //если совпадают выбранные параметры и параметры нового блока
                if (SelectedParameters.MembersAreSame(blockToSet.Parameters)) return;


                var parameterSelectors = ParameterSelectors.ToList();
                //отписываемся от событий выбора нового параметра
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlagedChanged -= OnSelectedParameterChanged);
                //обнуляем выбранные параметры
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlaged = null);

                //назначение в каждый селектор необходимого параметра
                foreach (var parameter in blockToSet.Parameters)
                {
                    //поиск селектора
                    var selector = ParameterSelectors.Single(ps => ps.ParametersFlaged.Select(x => x.Parameter).Contains(parameter));
                    //выбор параметра
                    selector.SelectedParameterFlaged = selector.ParametersFlaged.Single(p => p.Parameter.Equals(parameter));
                    selector.SelectedParameterFlaged.IsActual = true;
                }

                //подписываемся на события выбора нового параметра в каждом селекторе
                parameterSelectors.ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

                OnSelectedParameterChanged(null);

                _selectedBlock = blockToSet;

                OnPropertyChanged();
                SelectedProductBlockChanged?.Invoke(this);
            }
        }

        #endregion

        #region ctor

        public ProductBlockSelector(IEnumerable<Parameter> parameters, Bank bank, ProductBlock selectedProductBlock = null)
        {
            _bank = bank;

            //создаем селекторы параметров
            var groupedParameters = GetGroupingParameters(parameters).Select(x => new ParameterSelector(x));
            ParameterSelectors = new ObservableCollection<ParameterSelector>(groupedParameters);

            //подписка на смену параметра в селекторе
            ParameterSelectors.ToList().ForEach(ps => ps.SelectedParameterFlagedChanged += OnSelectedParameterChanged);

            //если есть выбранный блок
            if (selectedProductBlock != null)
            {
                if(!selectedProductBlock.Parameters.AllContainsIn(parameters))
                    throw new ArgumentException("Параметры блока не соответствуют возможным параметрам.");
                SelectedBlock = selectedProductBlock;
            }
        }

        #endregion

        #region events

        /// <summary>
        /// Событие изменения выбранного блока.
        /// </summary>
        public event Action<ProductBlockSelector> SelectedProductBlockChanged;

        #endregion

        /// <summary>
        /// группировка параметров по группе и упорядочивание их
        /// </summary>
        /// <param name="parameters">параметры</param>
        /// <returns></returns>
        private IEnumerable<IEnumerable<Parameter>> GetGroupingParameters(IEnumerable<Parameter> parameters)
        {
            //группировка параметров по группам и упорядочивание их.
            var groups = parameters.GroupBy(x => x.ParameterGroup.Id).
                                    OrderBy(x => x, new ParametersEnumerableComparer());
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.Value);
            }
        }

        /// <summary>
        /// Выбор первого базового параметра.
        /// </summary>
        public void SelectFirstParameter()
        {
            //все параметры
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);

            //параметр, у которого нет родительских параметров
            var selectedParameterFlaged = parametersFlaged.First(x => !x.Parameter.ParameterRelations.Any());

            //поиск селектора, содержащего такой параметр и назначение параметра
            var parameterSelector = ParameterSelectors.Single(x => x.ParametersFlaged.Contains(selectedParameterFlaged));
            parameterSelector.SelectedParameterFlaged = selectedParameterFlaged;
        }

        /// <summary>
        /// Реакция на изменение выбранного параметра в селекторе.
        /// </summary>
        /// <param name="parameterSelector"></param>
        private void OnSelectedParameterChanged(ParameterSelector parameterSelector)
        {
            //перепроверка актуальности параметров
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            foreach (var parameterFlaged in parametersFlaged)
            {
                parameterFlaged.IsActual = parameterFlaged.Parameter.IsOrigin ||
                    parameterFlaged.Parameter.ParameterRelations.
                    Any(x => x.RequiredParameters.AllContainsIn(SelectedParameters));
            }

            SelectedProductBlockChanged?.Invoke(this);
            OnPropertyChanged(nameof(SelectedBlock));
        }
    }
}