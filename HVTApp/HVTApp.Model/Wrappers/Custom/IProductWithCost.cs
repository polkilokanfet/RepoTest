using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        PartWrapper Part { get; }
        CostWrapper Cost { get; }
    }
}
