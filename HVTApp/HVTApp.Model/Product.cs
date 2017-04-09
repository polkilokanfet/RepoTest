using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Product : BaseEntity
    {
        public virtual Product ParentProduct { get; set; }
        public virtual List<Product> ChildProducts { get; set; } = new List<Product>();
        public virtual List<ProductParameter> Parameters { get; set; } = new List<ProductParameter>();

        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>(); //себестоимости

        //public override bool EqualsProperties(object obj)
        //{
        //    Product otherProduct = obj as Product;
        //    if (otherProduct == null)
        //        throw new ArgumentNullException();

        //    return !this.Links.Except(otherProduct.Links).Any();
        //}
    }

    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    public class SumOnDate : BaseEntity
    {
        public DateTime Date { get; set; }
        public virtual SumAndVat SumAndVat { get; set; }
    }

    public class ProductParameterType : BaseEntity
    {
        public string Name { get; set; }
    }
    public class ProductParameterMeasure : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

    public class ProductParameter : BaseEntity
    {
        public virtual ProductParameterType Type { get; set; }
        public virtual ProductParameterMeasure Measure { get; set; }
        public string Value { get; set; }
    }

    public class ProductParameterSet : BaseEntity
    {
        public bool IsRequired { get; set; }
        public virtual ProductParameter Parameter { get; set; }
        public virtual List<ProductParameter> RequiredParentParameters { get; set; } = new List<ProductParameter>(); //обязательные родительские параметры, без которых этот параметр не имеет смысла
    }
}
