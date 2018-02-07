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

        public ProductBlockSelector ProductBlockSelector { get; }
        public ObservableCollection<ProductSelector> ProductSelectors { get; } = new ObservableCollection<ProductSelector>();

        public ProductSelector(IEnumerable<Parameter> parameters, Product selectedProduct)
        {
            ProductBlockSelector = new ProductBlockSelector(parameters, selectedProduct);
            ProductBlockSelector.SelectedParametersChanged += ProductBlockSelectorOnSelectedParametersChanged;

            if (selectedProduct != null)
            {
                var dic = GetDictionaryOfMatching(selectedProduct);
                foreach (var kvp in dic)
                {
                    ProductSelectors.Add(new ProductSelector(parameters.RemoveUseLess(kvp.Key.ChildProductParameters), kvp.Value));
                }
            }
            else
            {
                ProductBlockSelector.SelectFirstParameter();
            }
        }

        private void ProductBlockSelectorOnSelectedParametersChanged(IEnumerable<Parameter> parameters)
        {
            RefreshProductSelectors();
        }

        private IEnumerable<ProductRelation> GetActualProductRelations(IEnumerable<Parameter> forParameters = null)
        {
            var parameters = forParameters ?? ProductBlockSelector.SelectedParameters;
            return ProductRelations.Where(x => Extansions.AllContainsIn(x.ParentProductParameters, parameters));
        }

        private void RefreshProductSelectors()
        {
            var actualProductRelations = GetActualProductRelations().ToList();
            var productSelectors = new List<ProductSelector>(ProductSelectors);

            //удаление неактуальных селекторов
            foreach (var productSelector in productSelectors)
            {
                if (actualProductRelations.All(x => !x.ChildProductParameters.AllContainsIn(productSelector.ProductBlockSelector.SelectedParameters)))
                    ProductSelectors.Remove(productSelector);
            }

            //добавление новых актуальных селекторов
            foreach (var productRelation in actualProductRelations)
            {
                if (ProductSelectors.Any(x => productRelation.ChildProductParameters.AllContainsIn(x.ProductBlockSelector.SelectedParameters)))
                    continue;
                ProductSelectors.Add(new ProductSelector(Parameters.RemoveUseLess(productRelation.ChildProductParameters), null));
            }
        }


        private Dictionary<ProductRelation, Product> GetDictionaryOfMatching(Product product)
        {
            var result = new Dictionary<ProductRelation, Product>();
            GetActualProductRelations(product.ProductBlock.Parameters).ToList().ForEach(x => result.Add(x, null));

            var products = new List<Product>(product.DependentProducts);

            while (products.Any())
            {
                var product1 = products.First();
                var searchZone = result.Where(x => x.Value == null);
                var node = searchZone.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(product1.ProductBlock.Parameters));
                if (!Equals(node, default(KeyValuePair<ProductRelation, Product>)))
                {
                    result[node.Key] = product1;
                    products.Remove(product1);
                    continue;
                }

                node = result.FirstOrDefault(x => x.Key.ChildProductParameters.AllContainsIn(product1.ProductBlock.Parameters));

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
    }
}