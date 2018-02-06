using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector
    {
        public static IEnumerable<ProductRelation> ProductRelations { get; set; } = new List<ProductRelation>();
        public static IEnumerable<Parameter> Parameters { get; set; } = new List<Parameter>();

        public bool IsActual => true;

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; }

        public ProductSelector(IEnumerable<Parameter> parameters, Product selectedProduct)
        {
            //создаем селекторы параметров
            var parameterSelectors = Grouping(parameters).Select(x => new ParameterSelector(x, this, Cross(x, selectedProduct)));
            ParameterSelectors = new ObservableCollection<ParameterSelector>(parameterSelectors);

            //реакция на смену параметра в селекторе
            foreach (var parameterSelector in ParameterSelectors)
                parameterSelector.SelectedParameterFlagedChanged += ParameterSelectorOnSelectedParameterFlagedChanged;

            ProductSelectors = new ObservableCollection<ProductSelector>();
            if (selectedProduct != null)
            {
                foreach (var dependentProduct in selectedProduct.DependentProducts)
                {
                    ProductSelectors.Add(new ProductSelector(Parameters, dependentProduct));
                }
            }
            else
            {
                SelectFirstBaseParameter();
            }
        }

        //выбор первого базового параметра
        private void SelectFirstBaseParameter()
        {
            var parametersFlaged = ParameterSelectors.SelectMany(x => x.ParametersFlaged);
            var selectedParameterFlaged = parametersFlaged.First(x => !x.Parameter.ParameterRelations.Any());
            var selector = ParameterSelectors.Single(x => x.ParametersFlaged.Contains(selectedParameterFlaged));
            selector.SelectedParameterFlaged = selectedParameterFlaged;
        }

        private void ParameterSelectorOnSelectedParameterFlagedChanged(ParameterSelector parameterSelector)
        {
            RefreshProductSelectors();
            OnSelectedParametersChanged(this.SelectedParametersFlaged.Select(x => x.Parameter));
        }

        private void RefreshProductSelectors()
        {
            var actualProductRelations = GetActualProductRelations().ToList();
            var productSelectors = new List<ProductSelector>(ProductSelectors);

            //удаление неактуальных селекторов
            foreach (var productSelector in productSelectors)
            {
                if (actualProductRelations.All(x => !x.ChildProductParameters.AllContainsIn(productSelector.SelectedParameters)))
                    ProductSelectors.Remove(productSelector);
            }

            //добавление новых актуальных селекторов
            foreach (var productRelation in actualProductRelations)
            {
                if (ProductSelectors.Any(x => productRelation.ChildProductParameters.AllContainsIn(x.SelectedParameters)))
                    continue;
                ProductSelectors.Add(new ProductSelector(KeepUsefullParameters(productRelation.ChildProductParameters), null));
            }
        }

        private IEnumerable<Parameter> KeepUsefullParameters(IEnumerable<Parameter> requiredParameters)
        {
            var result = new List<Parameter>(Parameters);
            foreach (var requiredParameter in requiredParameters)
            {
                var toExcept = Parameters.Where(x => Equals(x.ParameterGroupId, requiredParameter.ParameterGroup.Id))
                        .Except(new List<Parameter> {requiredParameter});
                result = result.Except(toExcept).ToList();
            }
            return result;
        }

        public IEnumerable<ParameterFlaged> SelectedParametersFlaged => 
            ParameterSelectors.Select(x => x.SelectedParameterFlaged).Where(x => x != null);

        public IEnumerable<Parameter> SelectedParameters => SelectedParametersFlaged.Select(x => x.Parameter);

        //группировка параметрам по группе и упорядочивание их
        private IEnumerable<IEnumerable<Parameter>> Grouping(IEnumerable<Parameter> parameters)
        {
            var groups = parameters.GroupBy(x => x.ParameterGroup);
            foreach (var group in groups)
            {
                yield return group.OrderBy(x => x.Value);
            }
        }

        private Parameter Cross(IEnumerable<Parameter> parameters, Product product)
        {
            return product == null ? null : parameters.SingleOrDefault(x => product.Parameters.Contains(x));
        }


        private IEnumerable<ProductRelation> GetActualProductRelations()
        {
            return ProductRelations.Where(x => x.ParentProductParameters.AllContainsIn(SelectedParameters));
        }

        #region events
        public event Action<IEnumerable<Parameter>> SelectedParametersChanged;

        protected virtual void OnSelectedParametersChanged(IEnumerable<Parameter> parameters)
        {
            SelectedParametersChanged?.Invoke(parameters);
        }
        #endregion
    }
}