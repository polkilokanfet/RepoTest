using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Market.Items
{
    public class ProjectUnitsGroup
    {
        public ProjectItem ProjectItem { get; }
        public List<SalesUnit> SalesUnits { get; }

        public Facility Facility { get; }
        public Product Product { get; }
        public int Amount { get; }
        public double Cost { get; }
        public double Total { get; }
        public DateTime OrderInTakeDate { get; }
        public DateTime ShipmentDate { get; }

        public ProjectUnitsGroup(IEnumerable<SalesUnit> salesUnits, ProjectItem projectItem)
        {
            ProjectItem = projectItem;
            if (salesUnits == null) throw new ArgumentNullException(nameof(salesUnits));
            if (!salesUnits.Any()) throw new ArgumentException($"{nameof(salesUnits)} - аргумент без членов");

            SalesUnits = salesUnits.ToList();

            var salesUnit = SalesUnits.First();

            Facility = salesUnit.Facility;
            Product = salesUnit.Product;
            Amount = SalesUnits.Count;
            Cost = salesUnit.Cost;
            Total = Amount * Cost;
            OrderInTakeDate = salesUnit.OrderInTakeDate;
            ShipmentDate = salesUnit.ShipmentDateCalculated;
        }
    }
}