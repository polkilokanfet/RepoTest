namespace HVTApp.UI.Lookup
{
    public interface IUnitLookup
    {
        FacilityLookup Facility { get; }
        ProductLookup Product { get; }
        double Cost { get; }

    }
}
