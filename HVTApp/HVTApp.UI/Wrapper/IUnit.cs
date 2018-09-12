using System;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IUnitDated : IUnit
    {
        DateTime DeliveryDateExpected { get; set; }
    }

    public interface IUnit : IUnitWithProductsIncluded, IValidatableChangeTracking
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