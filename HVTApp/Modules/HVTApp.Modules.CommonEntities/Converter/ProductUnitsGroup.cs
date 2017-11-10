using System.Collections.Generic;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public class ProductUnitsGroup
    {
        public FacilityWrapper Facility { get; set; }
        public ProductWrapper Product { get; set; }

        public int Amount => ProductUnits.Count;

        public double Cost { get; set; }
        public double Total => Cost * Amount;

        public List<IProductUnit> ProductUnits { get; set; }
    }
}