using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        ProductWrapper Product { get; }
        CostWrapper Cost { get; }
    }
}
