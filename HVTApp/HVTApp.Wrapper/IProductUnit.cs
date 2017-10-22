namespace HVTApp.Wrapper
{
    public interface IProductUnit
    {
        ProductWrapper Product { get; }
        FacilityWrapper Facility { get; }
        double Cost { get; }
    }
}
