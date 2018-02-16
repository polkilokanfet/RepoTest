using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class PaymentConditionSet : BaseEntity
    {
        public string Name { get; set; }
        public virtual List<PaymentCondition> PaymentConditions { get; set; } = new List<PaymentCondition>();
    }
}