using System;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Modules.Sales.Project1.Wrappers
{
    public interface IProjectUnit
    {
        double Cost { get; set; }
        double? CostDelivery { get; set; }
        int ProductionTerm { get; set; }
        DateTime DeliveryDateExpected { get; set; }
        string Comment { get; set; }
        Facility Facility { get; set; }
        Product Product { get; set; }
        Project Project { get; set; }
        PaymentConditionSet PaymentConditionSet { get; set; }
        Company Producer { get; set; }
    }
}