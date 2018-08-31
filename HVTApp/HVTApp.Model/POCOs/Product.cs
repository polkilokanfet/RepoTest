using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;
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
        public virtual List<Product> DependentProducts { get; set; } = new List<Product>();



        public override string ToString()
        {
            if (!string.IsNullOrEmpty(DesignationSpecial)) return DesignationSpecial;
            if (!string.IsNullOrEmpty(Designation)) return Designation;
            return ProductBlock.ToString();
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj)) return true;

            var otherProduct = obj as Product;
            if (otherProduct == null) return false;

            //если составные части не совпадают
            if (!Equals(this.ProductBlock, otherProduct.ProductBlock)) return false;

            //если зависимые продукты не совпадают / совпадают
            return DependentProducts.AllMembersAreSame(otherProduct.DependentProducts);
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
                    stringBuilder.Append(Environment.NewLine + dependentProduct.GetFullDescription(spaceCount).AddSpacesBefore(spaceCount));
                }
            }

            return stringBuilder.ToString();
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
