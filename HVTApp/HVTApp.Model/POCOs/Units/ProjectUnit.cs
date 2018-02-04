using System;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class ProjectUnit : BaseEntity
    {
        public virtual Guid ProjectId { get; set; }

        public virtual Facility Facility { get; set; }
        public virtual Product Product { get; set; }
        public double Cost { get; set; }

        public virtual DateTime DeliveryDate { get; set; } //желаемая дата поставки

        public override string ToString()
        {
            return "ProjectUnit: " + Product.ToString();
        }

    }
}