using HVTApp.Model.Wrappers;

namespace HVTApp.Modules.Sales.Converter
{
    public class ProductUnitsGroup
    {
        public FacilityWrapper Facility { get; set; }
        public ProductWrapper Product { get; set; }
        public int Amount { get; set; }
        public double Cost { get; set; }
    }
}