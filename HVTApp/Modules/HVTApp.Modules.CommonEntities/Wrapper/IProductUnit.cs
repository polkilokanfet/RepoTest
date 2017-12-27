namespace HVTApp.UI.Wrapper
{
    public interface IProductUnit
    {
        ProductWrapper Product { get; set; }
        FacilityWrapper Facility { get; set; }
        double Cost { get; set; }
    }
}
