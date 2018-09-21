using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Обозначение типа продукта")]
    public partial class ProductTypeDesignation : BaseEntity
    {
        [Designation("Тип"), Required, OrderStatus(10)]
        public virtual ProductType ProductType { get; set; }

        [Designation("Параметры")]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}