using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Groups
{
    public interface IWrapperGroup<TModel> : IWrapper<TModel>
        where TModel : class, IUnitPoco
    {
        double Cost { get; set; }
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
    }
}