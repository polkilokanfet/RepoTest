using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.Comparers;

namespace HVTApp.Model.POCOs
{
    [Designation("Продукт"), DesignationPlural("Продукты")]
    public partial class Product : BaseEntity
    {
        [Designation("Обозначение"), NotMapped, OrderStatus(8)]
        public string Designation => GlobalAppProperties.ProductDesignationService.GetDesignation(this);

        [Designation("Специальное обозначение"), MaxLength(50), OrderStatus(6)]
        public string DesignationSpecial { get; set; }

        [Designation("Тип"), NotMapped, OrderStatus(10)]
        public ProductType ProductType => GlobalAppProperties.ProductDesignationService.GetProductType(this);

        [Designation("Блок"), Required, OrderStatus(5)]
        public virtual ProductBlock ProductBlock { get; set; }

        [Designation("Продукты в составе")]
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();






        public override bool Equals(object obj)
        {
            return base.Equals(obj) || Equals(obj as Product);
        }

        protected bool Equals(Product other)
        {
            if (other == null) return false;

            //если составные части не совпадают
            if (!this.ProductBlock.Equals(other.ProductBlock)) return false;

            //если зависимые продукты не совпадают / совпадают
            return DependentProducts.MembersAreSame(other.DependentProducts, new ProductDependentComparer());
        }

        public override string ToString()
        {
            var type = ProductType == null ? String.Empty : $"{ProductType} ";
            if (!string.IsNullOrEmpty(DesignationSpecial)) return $"{type}{DesignationSpecial}";
            if (!string.IsNullOrEmpty(Designation)) return $"{type}{Designation}";
            return ProductBlock.ToString();
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
            var sb = new StringBuilder();
            sb.Append($"{Designation} ");
            sb.Append($"{this} (параметры: {ProductBlock.ParametersToString().ToLower()})".AddSpacesBefore(spaceCount));
            if (DependentProducts.Any())
            {
                spaceCount++;
                sb.AppendLine("Составные части:".AddSpacesBefore(spaceCount));
                foreach (var dependentProduct in DependentProducts)
                {
                    sb.AppendLine($"{dependentProduct.Amount} шт. " + dependentProduct.Product.GetFullDescription(spaceCount).AddSpacesBefore(spaceCount));
                }
            }

            return sb.ToString();
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
