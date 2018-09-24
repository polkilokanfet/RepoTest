using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Стандартный маржинальный доход")]
    public class StandartMarginalIncome : BaseEntity
    {
        [Designation("МД"), Required, OrderStatus(10)]
        public double MarginalIncome { get; set; } = 50;

        [Designation("Параметры оборудования"), Required, OrderStatus(9)]
        public virtual List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }
}