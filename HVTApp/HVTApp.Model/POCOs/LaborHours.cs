using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Нормо-часы на производство блока оборудования
    /// </summary>
    [Designation("Нормо-часы")]
    public class LaborHours : BaseEntity
    {
        [Designation("Параметры блока"), Required, OrderStatus(8)]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();

        [Designation("Количество"), Required, OrderStatus(9)]
        public double Amount { get; set; }
    }
}