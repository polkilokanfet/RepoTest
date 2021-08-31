using System.Collections.Generic;
using System.Linq;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;
using HVTApp.Infrastructure.Extansions;

namespace HVTApp.Model.POCOs
{
    [Designation("Условия оплаты")]
    public partial class PaymentConditionSet : BaseEntity
    {
        [Designation("Список условий"), NotForListView]
        public virtual List<PaymentCondition> PaymentConditions { get; set; } = new List<PaymentCondition>();

        public override string ToString()
        {
            var conditions = PaymentConditions.ToList();
            conditions.Sort();
            return conditions.ToStringEnum();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PaymentConditionSet paymentConditionSet)) return false;
            return this.PaymentConditions.MembersAreSame(paymentConditionSet.PaymentConditions);
        }
    }
}