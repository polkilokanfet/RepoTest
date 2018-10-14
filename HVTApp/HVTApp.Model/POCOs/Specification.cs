using System;
using System.ComponentModel.DataAnnotations;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Спецификация")]
    public partial class Specification : BaseEntity
    {
        [Designation("№"), Required, MaxLength(10), OrderStatus(10)]
        public string Number { get; set; }

        [Designation("Дата"), Required, OrderStatus(9)]
        public DateTime Date { get; set; }

        [Designation("НДС"), Required, OrderStatus(7)]
        public double Vat { get; set; } = GlobalAppProperties.Actual.Vat;

        [Designation("Договор"), Required, OrderStatus(8)]
        public virtual Contract Contract { get; set; }

        public override string ToString()
        {
            return Number;
        }
    }
}