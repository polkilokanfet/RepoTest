using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper.Groups
{
    public interface IWrapperGroup<out TModel> : IWrapper<TModel>
        where TModel : class, IUnit
    {
        double Cost { get; set; }
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
    }
}