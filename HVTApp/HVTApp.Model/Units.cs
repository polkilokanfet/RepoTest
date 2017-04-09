using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model
{
    public class ProductionUnit : BaseEntity
    {
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public DateTime? StartProductionDate { get; set; }
        public DateTime? EndProductionDate { get; set; }
        public int OrderPosition { get; set; } // Порядковый номер в заказе.
        public string SerialNumber { get; set; } // Заводской номер изделия.
    }

    public class SalesUnit : BaseEntity
    {
        public virtual SalesUnit ParentSalesUnit { get; set; }
        public virtual List<SalesUnit> ChildSalesUnits { get; set; } = new List<SalesUnit>();

        public virtual ProductionUnit ProductionUnit { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual ShipmentUnit ShipmentUnit { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public virtual List<Payment> PaymentsPlanned { get; set; } = new List<Payment>();
        public virtual List<Payment> PaymentsActual { get; set; } = new List<Payment>();
    }

    public class ShipmentUnit : BaseEntity
    {
        public virtual SalesUnit SalesUnit { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual DateTime? RequiredDeliveryDate { get; set; } //Желаемая дата поставки.

    }

    public class ProjectUnit : BaseEntity
    {
        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }
    }
}
