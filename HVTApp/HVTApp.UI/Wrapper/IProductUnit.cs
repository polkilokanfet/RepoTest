using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
    public interface IProductUnit : IValidatableChangeTracking
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