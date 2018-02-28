using System;
using System.ComponentModel;
using HVTApp.UI.Lookup;

namespace HVTApp.UI.ViewModels
{
    public interface IProjectUnitGroupLookup : INotifyPropertyChanged
    {
        FacilityLookup Facility { get; }
        ProductLookup Product { get; }
        int Amount { get; }
        double Cost { get; }
        DateTime DeliveryDateExpected { get; }
    }
}