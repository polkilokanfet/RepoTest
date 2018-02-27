using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    public partial class SalesUnit : BaseEntity
    {
        public Guid? ParentSalesUnitId { get; set; }
        public virtual List<SalesUnit> DependentSalesUnits { get; set; } = new List<SalesUnit>();
        public virtual Facility Facility { get; set; }
        public virtual DateTime DeliveryDateExpected { get; set; } =
            DateTime.Today.AddDays(CommonOptions.StandartTermFromStartToEndProduction + 30).SkipWeekend(); //требуемая дата поставки
        public virtual Company Producer { get; set; }
        public virtual DateTime? RealizationDate { get; set; }

        public override string ToString()
        {
            return $"SalesUnit: {Product} for {Facility}";
        }
    }

    //Production information
    public partial class SalesUnit : BaseEntity
    {
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public string OrderPosition { get; set; }
        public string SerialNumber { get; set; }

        public int? PlannedTermFromStartToEndProduction { get; set; }
        public int? PlannedTermFromPickToEndProduction { get; set; }

        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; }
        public DateTime? EndProductionDate { get; set; }
    }

    //Commersial information
    public partial class SalesUnit : BaseEntity
    {
        public double Cost { get; set; }

        public virtual Specification Specification { get; set; }

        public virtual PaymentConditionSet PaymentsConditionSet { get; set; }

        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();
        public virtual List<PaymentPlannedList> PaymentsPlannedSaved { get; set; } = new List<PaymentPlannedList>();
    }

    //Shipment information
    public partial class SalesUnit : BaseEntity
    {
        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public double CostOfShipment { get; set; } = 0;

        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки
    }
}