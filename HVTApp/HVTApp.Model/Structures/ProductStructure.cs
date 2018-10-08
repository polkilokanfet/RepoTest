using System.Collections.Generic;
using HVTApp.Model.POCOs;

namespace HVTApp.Model.Structures
{
    public class ProductStructure
    {
        public Product Product { get; }
        public int Amount { get; }
        /// <summary>
        /// Вложенные продукты
        /// </summary>
        public List<ProductStructure> ChildProductStructures { get; } = new List<ProductStructure>();

        public string Parameters => Product.ProductBlock.ParametersToString();

        public ProductStructure(Product product, int amount = 1)
        {
            Product = product;
            Amount = amount;
            foreach (var dependentProduct in Product.DependentProducts)
            {
                ChildProductStructures.Add(new ProductStructure(dependentProduct.Product, dependentProduct.Amount));
            }
        }

        public static IEnumerable<ProductStructure> GenerateProductStructures(SalesUnit salesUnit)
        {
            yield return new ProductStructure(salesUnit.Product);
            foreach (var productIncluded in salesUnit.ProductsIncluded)
            {
                yield return new ProductStructure(productIncluded.Product, productIncluded.Amount);
            }
        }
    }
}