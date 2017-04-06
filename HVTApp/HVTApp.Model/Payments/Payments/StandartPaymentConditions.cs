using System.Collections.Generic;

namespace HVTApp.Model
{
    public class StandartPaymentConditions : BaseEntity
    {
        public string Name { get; set; }
        public List<PaymentsCondition> PaymentsConditions { get; set; }
    }
}
