using System;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IUnit : IValidatableChangeTracking
    {
        ProductWrapper Product { get; set; }
        FacilityWrapper Facility { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        double Cost { get; set; }
        double Price { get; set; }
        int? ProductionTerm { get; set; }

        event Action PriceChanged;
    }
}