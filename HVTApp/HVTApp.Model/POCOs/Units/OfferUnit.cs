﻿using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class OfferUnit : BaseEntity
    {
        public virtual ProjectUnit ProjectUnit { get; set; }
        public virtual TenderUnit TenderUnit { get; set; }
        public virtual SalesUnit SalesUnit { get; set; }

        public virtual Product Product { get; set; }
        public virtual Offer Offer { get; set; }
        public double Cost { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
        public int ProductionTerm { get; set; } //срок производства
    }
}