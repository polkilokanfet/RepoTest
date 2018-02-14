using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class CommonUnit : BaseEntity
    {
        public virtual Facility Facility { get; set; }
        public virtual Product Product { get; set; }
        public double Cost { get; set; }

        public virtual DateTime DeliveryDate { get; set; } //желаемая дата поставки

        public virtual Company Producer { get; set; }

    }
}