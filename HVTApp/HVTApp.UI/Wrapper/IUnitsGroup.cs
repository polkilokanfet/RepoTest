using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace HVTApp.UI.Wrapper
{
    public interface IUnitsGroup : IUnitWithProductsIncluded
    {
        List<IUnit> Units { get; }
        ObservableCollection<IUnitsGroup> Groups { get; }
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        double Cost { get; set; }
        double Price { set; }
        int Amount { get; }
        double Total { get; }
        double MarginalIncome { get; set; }
        int? ProductionTerm { get; set; }
    }

    public interface IUnitsDatedGroup : IUnitsGroup
    {
        DateTime DeliveryDateExpected { get; set; }
    }

}