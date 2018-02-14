using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class SalesUnit : BaseEntity
    {
        public virtual Facility Facility { get; set; }

        public virtual Company Producer { get; set; }

        #region Production information

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public string SerialNumber { get; set; }

        public int PlannedTermFromStartToEndProduction { get; set; } = 120;
        public int PlannedTermFromPickToEndProduction { get; set; } = 7;

        public DateTime? StartProductionDate { get; set; }
        public DateTime? PickingDate { get; set; }
        public DateTime? EndProductionDate { get; set; }
        public DateTime? EndProductionDateByPlan { get; set; }

        #endregion

        #region Commersial information

        public double Cost { get; set; }

        public virtual Specification Specification { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();

        public virtual List<PaymentActual> PaymentsActual { get; set; } = new List<PaymentActual>();
        public virtual List<PaymentPlanned> PaymentsPlanned { get; set; } = new List<PaymentPlanned>();


        #endregion

        #region Shipment information

        public int? ExpectedDeliveryPeriod { get; set; }
        public virtual Address Address { get; set; }
        public double CostOfShipment { get; set; } = 0;

        public virtual DateTime? ShipmentDate { get; set; } //дата отгрузки
        public virtual DateTime? ShipmentPlanDate { get; set; } //плановая дата отгрузки
        public virtual DateTime? RequiredDeliveryDate { get; set; } //желаемая дата поставки
        public virtual DateTime? DeliveryDate { get; set; } //дата поставки

        #endregion

        public virtual DateTime? RealizationDate { get; set; }

        public override string ToString()
        {
            return "SalesUnitId: " + Product.ToString();
        }
    }
}