using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{

    [Designation("Продукт"), DesignationPlural("Продукты")]
    public partial class Product : BaseEntity
    {
        [Designation("Обозначение")]
        public string Designation { get; set; }

        [Designation("Специальное обозначение")]
        public string DesignationSpecial { get; set; }

        [Designation("Тип")]
        public ProductType ProductType { get; set; }

        [Designation("Блок")]
        public virtual ProductBlock ProductBlock { get; set; }

        [Designation("Продукты в составе")]
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();

        public override string ToString()
        {
            string type = ProductType == null ? String.Empty : $"{ProductType} ";
            if (!string.IsNullOrEmpty(DesignationSpecial)) return $"{type}{DesignationSpecial}";
            if (!string.IsNullOrEmpty(Designation)) return $"{type}{Designation}";
            return ProductBlock.ToString();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj) || Equals(obj as Product);
        }

        protected bool Equals(Product other)
        {
            if (other == null) return false;

            //если составные части не совпадают
            if (!this.ProductBlock.Equals(other.ProductBlock)) return false;

            return !DependentProducts.Except(other.DependentProducts, new ProductDependentComparer()).Any() &&
                   !other.DependentProducts.Except(DependentProducts, new ProductDependentComparer()).Any();

            //если зависимые продукты не совпадают / совпадают
            return DependentProducts.MembersAreSame(other.DependentProducts);
        }

        /// <summary>
        /// Возвращает все блоки оборудования
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProductBlock> GetBlocks()
        {
            var result = new List<ProductBlock> {ProductBlock};
            foreach (var dependentProduct in DependentProducts)
            {
                for (int i = 0; i < dependentProduct.Amount; i++)
                {
                    result.AddRange(dependentProduct.Product.GetBlocks());
                }
            }
            return result;
        }

        public IEnumerable<Product> GetProducts()
        {
            yield return this;

            foreach (var dependentProduct in DependentProducts)
            {
                foreach (var product in dependentProduct.Product.GetProducts())
                {
                    yield return product;
                }
            }

        }



        public string GetFullDescription(int spaceCount = 0)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{this} (параметры :: {ProductBlock.ParametersToString().ToLower()})".AddSpacesBefore(spaceCount));
            if (DependentProducts.Any())
            {
                spaceCount++;
                stringBuilder.Append(Environment.NewLine + "Составные части:".AddSpacesBefore(spaceCount));
                foreach (var dependentProduct in DependentProducts)
                {
                    stringBuilder.Append(Environment.NewLine + dependentProduct.Product.GetFullDescription(spaceCount).AddSpacesBefore(spaceCount));
                }
            }

            return stringBuilder.ToString();
        }
    }

    public class ProductDependentComparer : IEqualityComparer<ProductDependent>
    {
        public bool Equals(ProductDependent x, ProductDependent y)
        {
            return x.Equals(y);
        }

        public int GetHashCode(ProductDependent obj)
        {
            return 0;
        }
    }

    public static class StringExt
    {
        public static string AddSpacesBefore(this string targetString, int spaceCount)
        {
            var specesToAdd = string.Empty;
            for (int i = 0; i < spaceCount; i++)
            {
                specesToAdd += "::";
            }
            return specesToAdd + targetString;
        }
    }
}
