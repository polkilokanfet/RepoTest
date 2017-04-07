using System;
using System.Collections.Generic;

namespace HVTApp.Model
{
    public class ProductionProductUnit : BaseEntity
    {
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
        public DateTime? StartProductionDate { get; set; }
        public DateTime? EndProductionDate { get; set; }
        /// <summary>
        /// Порядковый номер в заказе.
        /// </summary>
        public int OrderPosition { get; set; }

        /// <summary>
        /// Заводской номер изделия.
        /// </summary>
        public string SerialNumber { get; set; }
    }

    public class SalesProductUnit : BaseEntity
    {
        public virtual ProductionProductUnit ProductionProductUnit { get; set; }
        public virtual SumAndVat Cost { get; set; }
        public virtual ShipmentProductUnit ShipmentProductUnit { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public virtual List<Payment> PaymentsPlanned { get; set; } = new List<Payment>();
        public virtual List<Payment> PaymentsActual { get; set; } = new List<Payment>();
    }

    public class ShipmentProductUnit : BaseEntity
    {
        public virtual SalesProductUnit SalesProductUnit { get; set; }
        public virtual DateTime? DateDesiredDelivery { get; set; } //Желаемая дата поставки.

    }

    public class ProjectProductUnit : BaseEntity
    {
        public virtual Facility Facility { get; set; }
        public virtual SalesProductUnit SalesProductUnit { get; set; }
    }

    public class Product : BaseEntity
    {
        public virtual Equipment Equipment { get; set; }
        public virtual Product ParentProduct { get; set; }
        public virtual List<Product> ChildProducts { get; set; }
    }
}
