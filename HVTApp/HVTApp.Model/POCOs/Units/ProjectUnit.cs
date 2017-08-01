using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProjectUnit : BaseEntity
    {
        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Equipment Equipment { get; set; }
        public double Cost { get; set; }

        public virtual List<TenderUnit> TenderUnits { get; set; }
        public virtual List<OfferUnit> OfferUnits { get; set; }

        public virtual DateTime RequiredDeliveryDate { get; set; } //желаемая дата поставки
    }
}