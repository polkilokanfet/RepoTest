using System.Collections.Generic;
using System.Collections.ObjectModel;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Converter
{
    public interface IProductUnitsGroup
    {
        List<IProductUnit> ProductUnits { get; }
        ObservableCollection<IProductUnitsGroup> Groups { get; }
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        PaymentConditionSetWrapper PaymentConditionSet { get; set; }
        double Cost { get; set; }
        int Amount { get; }
        double Total { get; }
        double MarginalIncome { get; set; }
        int? ProductionTerm { get; set; }
    }
}