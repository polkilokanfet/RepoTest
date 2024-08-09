using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.PlanAndEconomy.ViewModels
{
    public class OrderItem
    {
        private readonly List<SalesUnit> _salesUnits;

        public Facility Facility { get; }
        public Product Product { get; }
        public int Amount => _salesUnits.Count;
        public DateTime EndProductionDate { get; }
        public DateTime EndProductionPlanDate { get; }
        public int? EndProductionPlanDateYear { get; }
        public int? EndProductionPlanDateMonth { get; }
        public Order Order { get; }
        public DateTime OrderOpenDate { get; }
        public User Manager { get; }

        public OrderItem(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();
            Facility = _salesUnits.First().Facility;
            Product = _salesUnits.First().Product;
            EndProductionDate = _salesUnits.First().EndProductionDateCalculated;
            EndProductionPlanDate = _salesUnits.First().EndProductionPlanDate.Value;
            EndProductionPlanDateYear = EndProductionPlanDate.Year;
            EndProductionPlanDateMonth = EndProductionPlanDate.Month;
            Order = _salesUnits.First().Order;
            OrderOpenDate = Order.DateOpen;
            Manager = _salesUnits.First().Project.Manager;
        }
    }
}