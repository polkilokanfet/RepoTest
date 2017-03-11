using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.PaymentsCollections;

namespace HVTApp.Model
{
    public class Specification : BaseEntity
    {
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public virtual Contract Contract { get; set; }
        public virtual List<ProductsMainGroup> SalesGroups { get; set; }
        public virtual PaymentsConditionsCollection PaymentsConditions { get; set; }

        public double Sum => SalesGroups.Sum(x => x.Sum);
        public double SumWithVat => SalesGroups.Sum(x => x.SumWithVat);
    }
}