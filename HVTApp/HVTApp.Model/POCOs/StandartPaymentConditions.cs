using System.Collections.Generic;
using HVTApp.Infrastructure;

namespace HVTApp.Model.POCOs
{
    public class StandartPaymentConditions : BaseEntity
    {
        public string Name { get; set; }
        public List<PaymentCondition> PaymentsConditions { get; set; } = new List<PaymentCondition>();

        public override string ToString()
        {
            return Name;
        }
    }
}