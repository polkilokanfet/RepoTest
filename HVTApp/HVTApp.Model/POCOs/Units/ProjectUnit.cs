using System;
using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class ProjectUnit : BaseEntity
    {
        public virtual CommonUnit CommonUnit { get; set; }
        public virtual Project Project { get; set; }

        public virtual Facility Facility { get; set; }

        public virtual DateTime DeliveryDate { get; set; } //желаемая дата поставки

        public virtual Company Producer { get; set; }

        public virtual List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();


        public override string ToString()
        {
            return "ProjectUnit: " + CommonUnit.Product.ToString();
        }

    }
}