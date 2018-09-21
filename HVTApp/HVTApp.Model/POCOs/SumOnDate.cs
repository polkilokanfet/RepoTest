using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    /// <summary>
    /// Сумма на какую-либо дату
    /// </summary>
    [Designation("Сумма на дату")]
    public partial class SumOnDate : BaseEntity
    {
        [Designation("Дата"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Сумма"), Required]
        public double Sum { get; set; }
    }
}