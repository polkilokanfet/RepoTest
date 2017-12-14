using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        ProductWrapper Product { get; }
        CostWrapper Cost { get; }
    }
}
