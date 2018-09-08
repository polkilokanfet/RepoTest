using System.Collections.Generic;
using System.Linq;

namespace HVTApp.UI.Lookup
{
    public interface IUnitLookup
    {
        FacilityLookup Facility { get; }
        ProductLookup Product { get; }
        double Cost { get; }
    }

    public class GroupUnitsLookups
    {
        private readonly IEnumerable<IUnitLookup> _units;

        public int Amount => _units.Count();
        public double Cost => _units.First().Cost;
        public double Total => Amount * Cost;
        public ProductLookup Product { get; }
        public FacilityLookup Facility { get; }


        public GroupUnitsLookups(IEnumerable<IUnitLookup> units)
        {
            _units = units;
            Product = units.First().Product;
            Facility = units.First().Facility;
        }
    }
}
