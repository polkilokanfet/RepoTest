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

        string Product { get; }
        Guid ProductId { get; }
        void SetProduct(Product product);
        
        string PaymentConditionSet { get; }
        Guid PaymentConditionSetId { get; }
        void SetPaymentConditionSet(PaymentConditionSet paymentConditionSet);

        string Producer { get; }
        Guid? ProducerId { get; }
        void SetProducer(Company producer);


        void CopyProps(IProjectUnit projectUnit);
    }
}