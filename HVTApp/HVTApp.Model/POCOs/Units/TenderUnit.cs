using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class TenderUnit : BaseEntity
    {
        public virtual Guid TenderId { get; set; }
        public virtual ProjectUnit ProjectUnit { get; set; }

        public virtual Product Product { get; set; }
        public double Cost { get; set; }

        public virtual Company ProducerWinner { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();

        public DateTime DeliveryDate { get; set; }

        public override string ToString()
        {
            return "TenderUnit: " + Product.ToString();
        }
    }
}