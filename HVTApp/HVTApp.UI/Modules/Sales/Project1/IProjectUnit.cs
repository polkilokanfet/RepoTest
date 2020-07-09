using System;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.Sales.Project1
{
    public interface IProjectUnit
    {
        SalesUnit Model { get; }
        double Cost { get; set; }
        double Price { get; set; }
        double FixedCost { get; set; }
        int ProductionTerm { get; set; }
        DateTime DeliveryDateExpected { get; set; }
        double? CostDelivery { get; set; }

        #region ComplexProperties

        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        CompanyWrapper Producer { get; set; }

        #endregion

        #region CollectionProperties

        IValidatableChangeTrackingCollection<ProductIncludedWrapper> ProductsIncluded { get; }

        #endregion

        bool CanRemove { get; }
    }
}