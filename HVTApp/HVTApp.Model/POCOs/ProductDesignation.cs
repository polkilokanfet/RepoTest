using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Обозначение продукта")]
    public partial class ProductDesignation : BaseEntity
    {
        [Designation("Обозначение"), Required, MaxLength(50), OrderStatus(10)]
        public string Designation { get; set; }

        [Designation("Параметры")]
        public virtual List<Parameter> Parameters { get; set; }
    }
}