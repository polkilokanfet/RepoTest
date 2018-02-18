using System;
using System.ComponentModel;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.ViewModels
{
    public interface IProjectUnit : INotifyPropertyChanged
    {
        FacilityWrapper Facility { get; set; }
        ProductWrapper Product { get; set; }
        int Amount { get; }
        double Cost { get; set; }
        double MarginalIncome { get; set; }
        DateTime DeliveryDateExpected { get; set; }
    }
}