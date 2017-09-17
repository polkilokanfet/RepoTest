using HVTApp.Model.Wrappers;

namespace HVTApp.Model
{
    public interface IProductUnit
    {
        ProductWrapper Product { get; }
        FacilityWrapper Facility { get; }
        double Cost { get; }
    }
}
