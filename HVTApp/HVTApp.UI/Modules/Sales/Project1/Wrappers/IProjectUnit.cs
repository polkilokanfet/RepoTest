using System;
using System.ComponentModel;
using HVTApp.Infrastructure;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public interface IProjectUnit : INotifyPropertyChanged, IIsValid
    {
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        int ProductionTerm { get; set; }
        DateTime DeliveryDateExpected { get; set; }
        string Comment { get; set; }
        string Facility { get; }
        Guid FacilityId { get; }
        void SetFacility(Facility product);
        Guid ProductId { get; }
        void SetProduct(Product product);
        PaymentConditionSet PaymentConditionSet { get; set; }
        Company Producer { get; set; }
        void CopyProps(IProjectUnit projectUnit);
    }

    public interface IProjectUnitViewModel
    {
    }
}