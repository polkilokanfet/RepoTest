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
    public partial class SumOnDate : BaseEntity, ISumOnDate, IComparable<SumOnDate>
    {
        [Designation("Дата"), Required]
        public DateTime Date { get; set; } = DateTime.Today;

        [Designation("Сумма"), Required]
        public double Sum { get; set; }

        public override string ToString()
        {
            return $"{Sum} на {Date.ToShortDateString()}";
        }

        public int CompareTo(SumOnDate other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var dateComparison = Date.CompareTo(other.Date);
            if (dateComparison != 0) return dateComparison;
            return Sum.CompareTo(other.Sum);
        }
    }
}