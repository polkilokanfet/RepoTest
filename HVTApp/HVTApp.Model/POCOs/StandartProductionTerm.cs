using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Стандартный срок производства")]
    public class StandartProductionTerm : BaseEntity
    {
        [Designation("Срок производства"), Required, OrderStatus(10)]
        public int ProductionTerm { get; set; } = 120;

        [Designation("Параметры оборудования"), Required, OrderStatus(9)]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}