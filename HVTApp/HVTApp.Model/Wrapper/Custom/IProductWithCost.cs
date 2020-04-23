using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        ProductWrapper Product { get; }
        SumWrapper Cost { get; }
    }
}
