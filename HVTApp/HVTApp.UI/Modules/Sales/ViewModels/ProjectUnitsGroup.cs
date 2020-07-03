using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.ViewModels
{
    public class ProjectUnitsGroup
    {
        public Facility Facility { get; }
        public Product Product { get; }
        public int Amount { get; }
        public double Cost { get; }
        public double Total { get; }
        public DateTime OrderInTakeDate { get; }
        public DateTime ShipmentDate { get; }

        public ProjectUnitsGroup(IEnumerable<SalesUnit> salesUnits)
        {
            if (salesUnits == null) throw new ArgumentNullException(nameof(salesUnits));
            if (!salesUnits.Any()) throw new ArgumentException($"{nameof(salesUnits)} - аргумент без членов");

            var salesUnit = salesUnits.First();

            Facility = salesUnit.Facility;
            Product = salesUnit.Product;
            Amount = salesUnits.Count();
            Cost = salesUnit.Cost;
            Total = Amount * Cost;
            OrderInTakeDate = salesUnit.OrderInTakeDate;
            ShipmentDate = salesUnit.ShipmentDateCalculated;
        }
    }
}