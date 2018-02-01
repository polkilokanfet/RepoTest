using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class Specification : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public double Vat { get; set; } //НДС
        public virtual Contract Contract { get; set; }
    }
}