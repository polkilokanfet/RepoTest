using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        PartWrapper Part { get; }
        CostWrapper Cost { get; }
    }
}
