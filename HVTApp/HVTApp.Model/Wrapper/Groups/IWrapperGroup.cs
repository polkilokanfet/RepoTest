using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper.Groups
{
    public interface IWrapperGroup<out TModel> : IWrapper<TModel>
        where TModel : class, IUnit
    {
        double Cost { get; set; }
        FacilitySimpleWrapper Facility { get; set; }
        ProductSimpleWrapper Product { get; set; }
        IValidatableChangeTrackingCollection<ProductIncludedSimpleWrapper> ProductsIncluded { get; }
        PaymentConditionSetSimpleWrapper PaymentConditionSet { get; set; }
    }
}