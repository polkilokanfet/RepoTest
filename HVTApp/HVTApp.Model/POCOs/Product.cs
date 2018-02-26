using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    public partial class Product : BaseEntity
    {
        public string Designation { get; set; }

        public virtual ProductBlock ProductBlock { get; set; }

        public virtual List<Product> DependentProducts { get; set; } = new List<Product>();
    }

    public partial class Product
    {
        public override string ToString()
        {
            return string.IsNullOrEmpty(Designation) ? ProductBlock.ToString() : Designation;
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
