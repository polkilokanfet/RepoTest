using System;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attrubutes;

namespace HVTApp.Model.POCOs
{
    [Designation("Спецификация")]
    public partial class Specification : BaseEntity
    {
        [Designation("№")]
        public string Number { get; set; }

        [Designation("Дата")]
        public DateTime Date { get; set; }

        [Designation("НДС")]
        public double Vat { get; set; } //НДС

        public virtual Contract Contract { get; set; }

        public override string ToString()
        {
            return $"Спецификация №{Number} от {Date.ToShortDateString()} к договору №{Contract.Number} от {Contract.Date.ToShortDateString()}";
        }
    }
}