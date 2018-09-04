using System;

namespace HVTApp.UI.Wrapper
{
    public interface IProductUnit
    {
        ProductWrapper Product { get; set; }
        FacilityWrapper Facility { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        double Cost { get; set; }
        double Price { get; }
        int? ProductionTerm { get; set; }

        event Action PriceChanged;
    }
}