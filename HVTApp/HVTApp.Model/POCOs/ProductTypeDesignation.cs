using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductTypeDesignation : BaseEntity
    {
        public virtual ProductType ProductType { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}