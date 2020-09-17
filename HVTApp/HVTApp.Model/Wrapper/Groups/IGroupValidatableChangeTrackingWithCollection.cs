using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper.Groups
{
    public interface IGroupValidatableChangeTracking<out TModel> : IValidatableChangeTracking
        where TModel : IUnit
    {
        TModel Model { get; }
        double Cost { get; }
        double Total { get; }
        double? CostDelivery { get; set; }
        bool CostDeliveryIncluded { get; set; }
        double Price { set; }
        double FixedCost { set; }
        int ProductionTerm { get; }
        ProductWrapper Product { get; set; }
        FacilityWrapper Facility { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }

        IEnumerable<ProductIncludedWrapper> ProductsIncluded { get; }
        void AddProductIncluded(ProductIncluded productIncluded, bool isForEach);
        void RemoveProductIncluded(ProductIncludedWrapper productIncluded);

        SalesUnit SalesUnit { get; }
    }

    public interface IGroupValidatableChangeTrackingWithCollection<TMember, out TModel> : IGroupValidatableChangeTracking<TModel>
        where TMember : IGroupValidatableChangeTracking<TModel>
        where TModel : IUnit
    {
        IValidatableChangeTrackingCollection<TMember> Groups { get; }
    }
}