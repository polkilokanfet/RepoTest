using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProductComplexUnit : BaseEntity, IProductSalesUnit, IProductProductionUnit
    {
        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }

        #region ProductionInformation
        public virtual Product Product { get; set; }

        public virtual Order Order { get; set; }
        public int OrderPosition { get; set; } //порядковый номер в заказе
        public string SerialNumber { get; set; } //заводской номер изделия


        public int PlannedProductionTerm { get; set; } = 120;
        public int PlanedTermFromPickToEndProductionEnd { get; set; } = 7;


        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; } //дата комплектации
        public DateTime? EndProductionDate { get; set; }

        #endregion

        #region SalesInformation
        public virtual SumAndVat Cost { get; set; }
        public virtual Specification Specification { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public virtual List<PaymentPlan> PaymentsPlanned { get; set; } = new List<PaymentPlan>();
        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();

        public DateTime? RealizationDate { get; set; }

        #endregion

        public virtual ProductShipmentUnit ProductShipmentUnit { get; set; }

        public virtual List<ProductTenderUnit> TendersUnits { get; set; } = new List<ProductTenderUnit>();
        public virtual List<ProductOfferUnit> OffersUnits { get; set; } = new List<ProductOfferUnit>();
    }

    public interface IProductSalesUnit
    {
        Product Product { get; set; }
        SumAndVat Cost { get; set; }
        Specification Specification { get; set; }
        List<PaymentCondition> PaymentsConditions { get; set; }
        List<PaymentPlan> PaymentsPlanned { get; set; }
        List<PaymentActual> PaymentsActual { get; set; }
        DateTime? RealizationDate { get; set; }
    }

    public interface IProductProductionUnit
    {
        Product Product { get; set; }
        Order Order { get; set; }
        int OrderPosition { get; set; }
        string SerialNumber { get; set; }
        int PlannedProductionTerm { get; set; }
        int PlanedTermFromPickToEndProductionEnd { get; set; }
        DateTime? StartProductionDate { get; set; }
        DateTime? PickingDate { get; set; }
        DateTime? EndProductionDate { get; set; }
    }

    public class ProductShipmentUnit : BaseEntity
    {
        public virtual ProductComplexUnit ProductComplexUnit { get; set; }

        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? RequiredDeliveryDate { get; set; } //желаемая дата поставки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки
    }

    public class ProductOfferUnit : BaseEntity
    {
        public virtual ProductComplexUnit ProductComplexUnit { get; set; }

        public virtual Offer Offer { get; set; }
        public virtual Product Product { get; set; }
        public virtual SumAndVat Cost { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public int ProductionTerm { get; set; } //срок производства
    }

    public class ProductTenderUnit : BaseEntity
    {
        public virtual ProductComplexUnit ProductComplexUnit { get; set; }

        public virtual Tender Tender { get; set; }
        public virtual Product Product { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual Company ProducerWinner { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
    }
}
