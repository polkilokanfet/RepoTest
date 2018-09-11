using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Признаки услуги")]
    public partial class ProductBlockIsService : BaseEntity
    {
        public virtual List<Parameter> Parameters { get; set; }
    }
}