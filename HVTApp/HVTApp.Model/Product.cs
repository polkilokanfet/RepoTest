using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class Product : BaseEntity
    {
        public virtual Product ParentProduct { get; set; }
        public virtual List<Product> ChildProducts { get; set; } = new List<Product>();
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        public virtual List<SumOnDate> Prices { get; set; } = new List<SumOnDate>(); //себестоимости
    }
}
