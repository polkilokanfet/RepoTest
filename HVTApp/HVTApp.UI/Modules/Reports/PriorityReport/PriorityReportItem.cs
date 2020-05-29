using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Reports.PriorityReport
{
    public class PriorityReportItem
    {
        public Product Product { get; }
        public DateTime PickingDate { get; }
        public DateTime EndProductionDateByContract { get; }
        public string Facility { get; }
        public string Contragent { get; }
        public Order Order { get; }
        public string Positions { get; }
        public int Amount { get; }
        public double Cost { get; }
        public double CostWithVat { get; }
        public double SumPaid { get; }
        public double SumNotPaid => Cost - SumPaid;
        public double SumPaidPercent => SumPaid / Cost * 100.0;

        public PriorityReportItem(IEnumerable<SalesUnit> salesUnits)
        {
            var salesUnits1 = salesUnits.ToList();
            var salesUnit = salesUnits1.First();

            Product = salesUnit.Product;
            PickingDate = salesUnit.PickingDate ?? salesUnit.EndProductionDateCalculated.AddDays(GlobalAppProperties.Actual.StandartTermFromPickToEndProduction);
            EndProductionDateByContract = salesUnit.EndProductionDateByContractCalculated;
            Facility = salesUnit.Facility.ToString();
            Contragent = salesUnit.Specification?.Contract.Contragent.ToString();
            Order = salesUnit.Order;
            Positions = salesUnits1.Select(x => x.OrderPosition).GetOrderPositions();
            Amount = salesUnits1.Count;
            Cost = salesUnits1.Sum(x => x.Cost);
            CostWithVat = salesUnits1.Sum(x => (1.0 + x.Vat / 100.0) * x.Cost);
            SumPaid = salesUnits1.Sum(x => x.SumPaid);
        }
    }
}