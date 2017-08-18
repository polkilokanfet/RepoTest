using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class ProjectUnit : BaseEntity
    {
        public virtual Project Project { get; set; }
        public virtual Facility Facility { get; set; }
        public virtual Product Product { get; set; }
        public double Cost { get; set; }

        public virtual List<TenderUnit> TenderUnits { get; set; } = new List<TenderUnit>();
        public virtual List<OfferUnit> OfferUnits { get; set; } = new List<OfferUnit>();

        public virtual DateTime RequiredDeliveryDate { get; set; } //желаемая дата поставки
    }
}