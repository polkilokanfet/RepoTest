using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.GetProductService
{
    public class ProductSelector : NotifyPropertyChanged
    {
        public static IEnumerable<Product> Products { get; set; } = new List<Product>();
        public static IEnumerable<ProductRelation> ProductRelations { get; set; } = new List<ProductRelation>();
        public static IEnumerable<Parameter> Parameters { get; set; } = new List<Parameter>();

        public Product SelectedProduct
        {
            get { return Products.FirstOrDefault(x => x.Parameters.AllMembersAreSame(SelectedParameters)); }
        }

        public ObservableCollection<ParameterSelector> ParameterSelectors { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; } = new ObservableCollection<ProductSelector>();

        public ProductSelector(IEnumerable<Parameter> parameters, Product selectedProduct)
        {
            //создаем селекторы параметров
            var parameterSelectors = GetGroupingParameters(parameters).Select(x => new ParameterSelector(x, this, Cross(x, selectedProduct)));
            ParameterSelectors = new ObservableCollection<ParameterSelector>(parameterSelectors);

            //реакция на смену параметра в селекторе
            foreach (var parameterSelector in ParameterSelectors)
                parameterSelector.SelectedParameterFlagedChanged += ParameterSelectorOnSelectedParameterFlagedChanged;

            if (selectedProduct != null)
            {
                var dic = GetDictionaryOfMatching(selectedProduct);
                foreach (var kvp in dic)
                {
                    ProductSelectors.Add(new ProductSelector(GetUsefullParameters(kvp.Key.ChildProductParameters), kvp.Value));
                }
            }
            else
            {
                SelectFirstBaseParameter();
            }
        }

        private Dictionary<ProductRelation, Product> GetDictionaryOfMatching(Product product)
        {
            var result = new Dictionary<ProductRelation, Product>();
            GetActualProductRelations(product.Parameters).ToList().ForEach(x => result.Add(x, null));

            var products = new List<Product>(product.DependentProducts);

            while (products.Any())
            {
                var product1 = products.First();
                var searchZone = result.Where(x => x.Value == null);
                var node = searchZone.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(product1.Parameters));
                if (!Equals(node, default(KeyValuePair<ProductRelation, Product>)))
                {
                    result[node.Key] = product1;
                    products.Remove(product1);
                    continue;
                }

                node = result.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(product1.Parameters));

                if (!Equals(node, default(KeyValuePair<ProductRelation, Product>)))
                {
                    products.Add(result[node.Key]);
                    result[node.Key] = product1;
                    products.Remove(product1);
                    continue;
                }

                throw new Exception("Не найдена подходящая зависимость для продукта");
            }

            return result;
        }

        private void ParameterSelectorOnSelectedParameterFlagedChanged(ParameterSelector parameterSelector)
        {
            RefreshProductSelectors();
            OnSelectedParametersChanged(this.SelectedParametersFlaged.Select(x => x.Parameter));
            OnPropertyChanged(nameof(SelectedProduct));
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
                ProductSelectors.Add(new ProductSelector(GetUsefullParameters(productRelation.ChildProductParameters), null));
            }
        }

        private IEnumerable<Parameter> GetUsefullParameters(IEnumerable<Parameter> requiredParameters)
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
            return product == null ? null : parameters.SingleOrDefault(x => product.Parameters.Contains(x));
        }

        private IEnumerable<ProductRelation> GetActualProductRelations(IEnumerable<Parameter> forParameters = null)
        {
            var parameters = forParameters ?? SelectedParameters;
            return ProductRelations.Where(x => x.ParentProductParameters.AllContainsIn(parameters));
        }

        //выбор первого базового параметра
        private void SelectFirstBaseParameter()
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
}