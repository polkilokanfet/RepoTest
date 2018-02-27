using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public partial class PaymentConditionSet : BaseEntity
    {
        public virtual List<PaymentCondition> PaymentConditions { get; set; } = new List<PaymentCondition>();

        public override string ToString()
        {
            string result = string.Empty;
            foreach (var paymentCondition in PaymentConditions.OrderBy(x => x))
            {
                result += $"{paymentCondition}; ";
            }
            return result;
        }
    }
}