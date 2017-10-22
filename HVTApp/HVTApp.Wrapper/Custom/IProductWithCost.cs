using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        PartWrapper Part { get; }
        CostWrapper Cost { get; }
    }
}
