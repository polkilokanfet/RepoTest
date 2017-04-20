using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class SalesUnit : BaseEntity
    {
        public virtual SalesUnit ParentSalesUnit { get; set; }
        public virtual List<SalesUnit> ChildSalesUnits { get; set; } = new List<SalesUnit>();

        public virtual List<TenderUnit> TenderUnits { get; set; } = new List<TenderUnit>();
        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();

        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Specification Specification { get; set; }

        public virtual SumAndVat CostSingle { get; set; }

        public virtual ProductionUnit ProductionUnit { get; set; }
        public virtual ShipmentUnit ShipmentUnit { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public virtual List<PaymentPlan> PaymentsPlanned { get; set; } = new List<PaymentPlan>();
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

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
}
