using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Groups
{
    public class SalesUnitsGroup
    {
        private readonly List<SalesUnit> _units;

        public Facility Facility => _units.First().Facility;
        public Product Product => _units.First().Product;
        public int Amount => _units.Count;
        public double Cost => _units.First().Cost;
        public double Total => Amount * Cost;
        public int ProductionTerm => _units.First().ProductionTerm;
        public PaymentConditionSet PaymentConditionSet => _units.First().PaymentConditionSet;

        public SalesUnitsGroup(IEnumerable<SalesUnit> salesUnits)
        {
            _units = new List<SalesUnit>(salesUnits);
        }

    }
}