using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class Unit : BaseEntity
    {
        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }

        public virtual ProjectsUnit ProjectsUnit { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }
        public virtual ProductionsUnit ProductionsUnit { get; set; }
        public virtual ShipmentsUnit ShipmentsUnit { get; set; }

        public virtual List<TendersUnit> TendersUnits { get; set; } = new List<TendersUnit>();
        public virtual List<OffersUnit> OffersUnits { get; set; } = new List<OffersUnit>();
    }

    public class ProjectsUnit : BaseEntity
    {
        public virtual Unit Unit { get; set; }
        public virtual Product Product { get; set; }
        public virtual SumAndVat Cost { get; set; }
    }

    public class SalesUnit : BaseEntity
    {
        public virtual Unit Unit { get; set; }

        public virtual SumAndVat Cost { get; set; }
        public virtual Specification Specification { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public virtual List<PaymentPlan> PaymentsPlanned { get; set; } = new List<PaymentPlan>();
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        public DateTime? RealizationDate { get; set; }
    }

    public class ProductionsUnit : BaseEntity
    {
        public virtual Unit Unit { get; set; }
        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
        public int OrderPosition { get; set; } //порядковый номер в заказе
        public string SerialNumber { get; set; } //заводской номер изделия


        public int PlannedProductionTerm { get; set; } = 120;
        public int PlanedTermFromPickToEndProductionEnd { get; set; } = 7;


        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; } //дата комплектации
        public DateTime? EndProductionDate { get; set; }

    }

    public class ShipmentsUnit : BaseEntity
    {
        public virtual Unit Unit { get; set; }

        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? RequiredDeliveryDate { get; set; } //желаемая дата поставки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки
    }

    public class OffersUnit : BaseEntity
    {
        public virtual Unit Unit { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Product Product { get; set; }
        public virtual SumAndVat Cost { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
    }

    public class TendersUnit : BaseEntity
    {
        public virtual Unit Unit { get; set; }

        public virtual Tender Tender { get; set; }
        public virtual Product Product { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual Company ProducerWinner { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
    }

}
