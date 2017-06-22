using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
    public interface IProductWithCost : IValidatableChangeTracking
    {
        ProductWrapper Product { get; }
        SumAndVatWrapper Cost { get; }
    }
}
