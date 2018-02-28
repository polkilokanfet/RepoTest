using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Specification : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public double Vat { get; set; } //НДС
        public virtual Contract Contract { get; set; }

        public override string ToString()
        {
            return $"Спецификация №{Number} от {Date.ToShortDateString()} к договору №{Contract.Number} от {Contract.Date.ToShortDateString()}";
        }
    }
}