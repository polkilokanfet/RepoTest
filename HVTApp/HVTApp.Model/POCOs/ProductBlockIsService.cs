using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Признаки услуги")]
    public partial class ProductBlockIsService : BaseEntity
    {
        [Designation("Параметры"), Required]
        public virtual List<Parameter> Parameters { get; set; }

        [Designation("Тип услуги"), Required]
        public ProductBlockServiceType Type { get; set; }
    }
}