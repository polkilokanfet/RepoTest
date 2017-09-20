using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Product : BaseEntity
    {
        public string Designation { get; set; }
        public virtual Part Part { get; set; }
        public virtual List<Product> DependentProducts { get; set; } = new List<Product>();


        public override bool Equals(object obj)
        {
            if(base.Equals(obj)) return true;

            Product otherProduct = obj as Product;
            if (otherProduct == null) return false;

            //если составные части не совпадают
            if (!Part.Equals(otherProduct.Part)) return false;

            //если зависимые продукты не совпадают / совпадают
            return DependentProducts.AllMembersAreSame(otherProduct.DependentProducts);
        }

        public string PartsToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(Part.ToString());
            if (DependentProducts.Any())
            {
                stringBuilder.Append(Environment.NewLine + "Составные части:");
                foreach (var dependentProduct in DependentProducts)
                    stringBuilder.Append(Environment.NewLine + dependentProduct.PartsToString());
            }

            return stringBuilder.ToString();
        }

        public override string ToString()
        {
            if (!String.IsNullOrEmpty(Designation)) return Designation;
            return Part.ToString();
        }
    }
}
