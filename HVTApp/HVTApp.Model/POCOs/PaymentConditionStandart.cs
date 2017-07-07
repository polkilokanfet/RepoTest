using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class PaymentConditionStandart : BaseEntity
    {
        public string Name { get; set; }
        public List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();
    }
}