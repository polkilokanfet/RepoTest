namespace HVTApp.UI.Lookup
{
    public interface IProductUnitLookup
    {
        FacilityLookup Facility { get; }
        ProductLookup Product { get; }
        double Cost { get; }
    }
}
