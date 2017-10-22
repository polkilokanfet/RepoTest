using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Order : BaseEntity
    {
        public string Number { get; set; }
        public DateTime OpenOrderDate { get; set; }
        public virtual List<ProductionUnit> ProductionUnits { get; set; }
    }
}