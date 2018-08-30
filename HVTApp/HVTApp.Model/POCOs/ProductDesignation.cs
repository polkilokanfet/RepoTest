using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductDesignation : BaseEntity
    {
        public string Designation { get; set; }
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}