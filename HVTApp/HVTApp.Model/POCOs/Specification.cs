using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Specification : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public double Vat { get; set; } //НДС
        public virtual Contract Contract { get; set; }
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();

        public override string ToString()
        {
            return $"Specification №{Number} of contract {Contract}";
        }
    }
}