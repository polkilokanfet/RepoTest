using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Groups
{
    public interface IGroupValidatableChangeTracking<TModel> : IValidatableChangeTracking
        where TModel : IUnitPoco
    {
        TModel Model { get; }
        double Cost { get; }
        double Price { set; }
        int ProductionTerm { get; }
        ProductWrapper Product { get; set; }
        FacilityWrapper Facility { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }

        IEnumerable<ProductIncludedWrapper> ProductsIncluded { get; }
        void AddProductIncluded(ProductIncluded productIncluded);
        void RemoveProductIncluded(ProductIncludedWrapper productIncluded);
    }

    public interface IGroupValidatableChangeTrackingWithCollection<T, TModel> : IGroupValidatableChangeTracking<TModel>
        where T : IGroupValidatableChangeTracking<TModel>
        where TModel : IUnitPoco
    {
        IValidatableChangeTrackingCollection<T> Groups { get; }
    }
}