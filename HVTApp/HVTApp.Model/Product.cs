using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Product : BaseEntity
    {
        public virtual Product ParentProduct { get; set; }
        public virtual List<Product> ChildProducts { get; set; } = new List<Product>();
        public virtual List<ProductParameter> Parameters { get; set; } = new List<ProductParameter>();

        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>(); //себестоимости
    }

    public class ProductParameter : BaseEntity
    {
        public virtual ProductParameterGroup Group { get; set; }
        public string Value { get; set; }
        public virtual List<ProductParameterSet> ProductParameterSets { get; set; }
    }

    public class ProductParameterGroup : BaseEntity
    {
        public string Name { get; set; }
        public virtual ProductParameterMeasure Measure { get; set; }
        public bool IsOntyChoice { get; set; } = true; // группа из которой может быть выбран только один параметр для одного оборудования.
    }

    /// <summary>
    /// Единица измерения
    /// </summary>
    public class ProductParameterMeasure : BaseEntity
    {
        public string FullName { get; set; }
        public string ShortName { get; set; }
    }

    public class ProductParameterSet : BaseEntity
    {
        public bool IsRequired { get; set; }
        public virtual ProductParameter Parameter { get; set; }
        public virtual List<ProductParameter> RequiredParentParameters { get; set; } = new List<ProductParameter>(); //обязательные родительские параметры, без которых этот параметр не имеет смысла
    }
}
