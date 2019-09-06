using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure.Extansions;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Groups
{
    /// <summary>
    /// Группа для отображения в простых видах
    /// </summary>
    public class SalesUnitsGroup
    {
        private readonly List<SalesUnit> _salesUnits;

        public Facility Facility => _salesUnits.First().Facility;
        public Product Product => _salesUnits.First().Product;
        public int Amount => _salesUnits.Count;
        public double Cost => _salesUnits.First().Cost;
        public double Total => Amount * Cost;
        public int ProductionTerm => _salesUnits.First().ProductionTerm;
        public PaymentConditionSet PaymentConditionSet => _salesUnits.First().PaymentConditionSet;

        public SalesUnitsGroup(IEnumerable<SalesUnit> salesUnits)
        {
            _salesUnits = salesUnits.ToList();
        }
    }
}