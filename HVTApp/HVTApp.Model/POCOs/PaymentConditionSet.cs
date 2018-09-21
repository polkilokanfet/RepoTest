using System.Collections.Generic;
using System.Linq;
using System.Text;
using HVTApp.Infrastructure;
using HVTApp.Infrastructure.Attributes;

namespace HVTApp.Model.POCOs
{
    [Designation("Условия оплаты")]
    public partial class PaymentConditionSet : BaseEntity
    {
        [Designation("Список условий")]
        public virtual List<PaymentCondition> PaymentConditions { get; set; } = new List<PaymentCondition>();

        public override string ToString()
        {
            var sb = new StringBuilder();
            PaymentConditions.OrderBy(x => x).ToList().ForEach(x => sb.Append($"{x}; "));
            return sb.ToString();
        }
    }
}