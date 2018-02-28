using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    //Project information
    public partial class SalesUnit : BaseEntity, IProductCostDependentProducts
    {
        public double Cost { get; set; }
        public virtual Product Product { get; set; }
        public virtual List<ProductDependent> DependentProducts { get; set; } = new List<ProductDependent>();

        public virtual Facility Facility { get; set; }
        public virtual PaymentConditionSet PaymentConditionSet { get; set; }
        public int? ProductionTerm { get; set; }


        public virtual DateTime DeliveryDateExpected { get; set; } = DateTime.Today.AddDays(CommonOptions.ProductionTerm + 30).SkipWeekend(); //требуемая дата поставки
        public virtual Company Producer { get; set; }
        public virtual DateTime? RealizationDate { get; set; }

        public override string ToString()
        {
            return $"SalesUnit: {Product} for {Facility}";
        }
    }

    //Production information
    public partial class SalesUnit 
    {
        public virtual Order Order { get; set; }
        public string OrderPosition { get; set; }
        public string SerialNumber { get; set; }

        public int? AssembleTerm { get; set; }

        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; }
        public DateTime? EndProductionDate { get; set; }
    }

    //Commersial information
    public partial class SalesUnit 
    {
        public virtual Specification Specification { get; set; }

        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();
        public virtual List<PaymentPlannedList> PaymentsPlannedSaved { get; set; } = new List<PaymentPlannedList>();
    }

    //Shipment information
    public partial class SalesUnit 
    {
        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public double CostOfShipment { get; set; } = 0;

        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки
    }
}