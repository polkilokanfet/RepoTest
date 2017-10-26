using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public class ProductUnitsGroup
    {
        public FacilityWrapper Facility { get; set; }
        public ProductWrapper Product { get; set; }
        public int Amount { get; set; }
        public double Cost { get; set; }
    }
}