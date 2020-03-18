using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}