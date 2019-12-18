using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class OrderItem
    {
        private readonly List<SalesUnit> _salesUnits;

        public Product Product { get; }
        public int Amount => _salesUnits.Count;
        public DateTime EndProductionPlanDate { get; }
        public int EndProductionPlanDateYear { get; }
        public int EndProductionPlanDateMonth { get; }
        public Order Order { get; }

        public OrderItem(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();
            Product = _salesUnits.First().Product;
            EndProductionPlanDate = _salesUnits.First().EndProductionPlanDate.Value;
            EndProductionPlanDateYear = EndProductionPlanDate.Year;
            EndProductionPlanDateMonth = EndProductionPlanDate.Month;
            Order = _salesUnits.First().Order;
        }
    }
}