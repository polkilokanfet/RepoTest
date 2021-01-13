using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;
using HVTApp.Model.Wrapper.Groups.SimpleWrappers;

namespace HVTApp.Model.Wrapper.Groups
{
    public interface IGroupValidatableChangeTracking<out TModel> : IValidatableChangeTracking
        where TModel : IUnit
    {
        TModel Model { get; }
        string Comment { get; set; }
        double Cost { get; set; }
        double Total { get; }
        double? CostDelivery { get; set; }
        bool CostDeliveryIncluded { get; set; }
        double Price { get; set; }
        int Amount { get; }
        double FixedCost { get; set; }
        int ProductionTerm { get; }
        ProductSimpleWrapper Product { get; set; }
        FacilitySimpleWrapper Facility { get; set; }
        PaymentConditionSetSimpleWrapper PaymentConditionSet { get; set; }

        IEnumerable<ProductIncludedSimpleWrapper> ProductsIncluded { get; }
        void AddProductIncluded(ProductIncluded productIncluded, bool isForEach);
        void RemoveProductIncluded(ProductIncludedSimpleWrapper productIncluded);

        SalesUnit SalesUnit { get; }
    }

    public interface IGroupValidatableChangeTrackingWithCollection<TMember, out TModel> : IGroupValidatableChangeTracking<TModel>
        where TMember : IGroupValidatableChangeTracking<TModel>
        where TModel : IUnit
    {
        IValidatableChangeTrackingCollection<TMember> Groups { get; }
    }
}