using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class TenderUnit : BaseEntity
    {
        public virtual ProjectUnit ProjectUnit { get; set; }

        public virtual Product Product { get; set; }
        public virtual Tender Tender { get; set; }
        public double Cost { get; set; }

        public virtual Company ProducerWinner { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();

        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();

        public DateTime DeliveryDate { get; set; }
    }
}