using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.Services.PrintService
{
    public class UnitsGroup
    {
        public IReadOnlyCollection<Unit> Units { get; }

        public int Amount => Units.Count;
        public double Total => Units.Sum(x => x.Cost);

        public Facility Facility => Units.First().Facility;
        public Product Product => Units.First().Product;
        public double Cost => Units.First().Cost;
        public double? CostDelivery => Units.First().CostDelivery;
        public bool CostDeliveryIncluded => Units.First().CostDeliveryIncluded;
        public int ProductionTerm => Units.First().ProductionTerm;
        public string Comment => Units.First().Comment;
        public PaymentConditionSet PaymentConditionSet => Units.First().PaymentConditionSet;
        public IEnumerable<ProductIncluded> ProductsIncluded => Units.First().ProductsIncluded;

        /// <summary>
        /// Позиция строки
        /// </summary>
        public int Position { get; set; }

        public UnitsGroup(IEnumerable<Unit> units)
        {
            Units = new List<Unit>(units);
        }
    }
}