using System;
using HVTApp.Infrastructure;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
    public interface IUnitWrapperDated : IUnitWrapper
    {
        DateTime DeliveryDateExpected { get; set; }
    }

    public interface IUnitWrapper : IUnitWithProductsIncluded, IValidatableChangeTracking
    {
        ProductWrapper Product { get; set; }
        FacilityWrapper Facility { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        double Cost { get; set; }
        double Price { get; set; }
        int? ProductionTerm { get; set; }
    }

    public interface IUnitWithProductsIncluded
    {
        IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }
    }
}