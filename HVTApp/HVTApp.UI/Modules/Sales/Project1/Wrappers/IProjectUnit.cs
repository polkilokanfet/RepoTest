using System;
using System.ComponentModel;
using HVTApp.Model.Wrapper;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public interface IProjectUnit : INotifyPropertyChanged
    {
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        int ProductionTerm { get; set; }
        DateTime DeliveryDateExpected { get; set; }
        string Comment { get; set; }

        FacilityEmptyWrapper Facility { get; set; }
        ProductEmptyWrapper Product { get; set; }
        PaymentConditionSetEmptyWrapper PaymentConditionSet { get; set; }
        CompanyEmptyWrapper Producer { get; set; }


        void CopyProps(IProjectUnit projectUnit);
    }
}