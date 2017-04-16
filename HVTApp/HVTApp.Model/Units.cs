using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class SalesUnit : BaseEntity
    {
        public virtual SalesUnit ParentSalesUnit { get; set; }
        public virtual List<SalesUnit> ChildSalesUnits { get; set; } = new List<SalesUnit>();

        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }

        public virtual SumAndVat CostSingle { get; set; }

        public virtual ProductionUnit ProductionUnit { get; set; }
        public virtual ShipmentUnit ShipmentUnit { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();

        public virtual Specification Specification { get; set; }

        public virtual List<Payment> PaymentsPlanned { get; set; } = new List<Payment>();
        public virtual List<Payment> PaymentsActual { get; set; } = new List<Payment>();

        public DateTime? RealizationDate { get; set; }
    }

    public class ProductionUnit : BaseEntity
    {
        public int PlannedProductionTerm { get; set; } = 120;
        public int PlanedTermFromPickToEndProductionEnd { get; set; } = 7;
        public virtual Product Product { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }
        public virtual Order Order { get; set; }
        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; } //дата комплектации
        public DateTime? EndProductionDate { get; set; }
        public int OrderPosition { get; set; } //порядковый номер в заказе
        public string SerialNumber { get; set; } //заводской номер изделия
    }

    public class ShipmentUnit : BaseEntity
    {
        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }
        public virtual double ShipmentCost { get; set; }
        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? RequiredDeliveryDate { get; set; } //желаемая дата поставки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки
    }

    public class TenderUnit : BaseEntity
    {
        public virtual SalesUnit SalesUnit { get; set; }
        public virtual SumAndVat Cost { get; set; }
    }
}
