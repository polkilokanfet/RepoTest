using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public interface IProductCostWrapper
    {
        ProductWrapper Product { get; set; }
        double Cost { get; set; }
        double MarginalIncome { get; set; }
    }

    public interface IProductCostDependentProductsWrapper : IProductCostWrapper
    {
        IValidatableChangeTrackingCollection<ProductDependentWrapper> DependentProducts { get; }
    }
}