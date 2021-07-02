using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Стоимость нормо-часа
    /// </summary>
    [Designation("Нормо-час стоимость")]
    public class LaborHourCost : BaseEntity, ISumOnDate
    {
        [Designation("Дата"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Сумма"), Required]
        public double Sum { get; set; }

        public override string ToString()
        {
            return $"{Sum} на {Date.ToShortDateString()}";
        }
    }
}