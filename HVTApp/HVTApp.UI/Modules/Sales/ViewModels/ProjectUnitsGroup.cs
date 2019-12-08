using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProjectUnitsGroup
    {
        private readonly List<SalesUnit> _salesUnits;
        public Facility Facility { get; }
        public Product Product { get; }
        public int Amount { get; }
        public double Cost { get; }
        public double Total { get; }
        public DateTime OrderInTakeDate { get; }

        public ProjectUnitsGroup(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();

            Facility = _salesUnits.First().Facility;
            Product = _salesUnits.First().Product;
            Amount = _salesUnits.Count;
            Cost = _salesUnits.First().Cost;
            Total = Amount * Cost;
            OrderInTakeDate = _salesUnits.First().OrderInTakeDate;
        }
    }
}