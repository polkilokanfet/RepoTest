using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Specification : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual List<SalesUnit> SalesUnits { get; set; } = new List<SalesUnit>();
    }
}