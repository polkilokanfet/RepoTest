using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Product : BaseEntity
    {
        public string Designation { get; set; }
        public virtual ProductItem ProductItem { get; set; }
        public virtual List<Product> ChildProducts { get; set; } = new List<Product>();
    }

    public class ProductItem : BaseEntity
    {
        public string Designation { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>(); //себестоимости по датам

        public string StructureCostNumber { get; set; }

        public override string ToString()
        {
            //if (!string.IsNullOrEmpty(Designation))
                return Designation;
        }
    }

    public class RequiredProductsChilds : BaseEntity
    {
        public virtual List<Parameter> MainProductParameters { get; set; } = new List<Parameter>();
        public virtual List<Parameter> ChildProductParameters { get; set; } = new List<Parameter>();
        public int Count { get; set; }
    }
}
