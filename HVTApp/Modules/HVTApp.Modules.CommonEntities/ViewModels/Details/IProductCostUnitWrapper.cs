using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public interface IProductCostUnitWrapper
    {
        ProductWrapper Product { get; set; }
        IValidatableChangeTrackingCollection<ProductWrapper> DependentProducts { get; }

        double Cost { get; set; }
        double MarginalIncome { get; set; }
    }
}