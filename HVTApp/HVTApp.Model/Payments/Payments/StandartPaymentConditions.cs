using HVTApp.Model.PaymentsCollections;

namespace HVTApp.Model
{
    public class StandartPaymentConditions : BaseEntity
    {
        public string Name { get; set; }
        public PaymentsConditionsCollection PaymentsConditionsCollection { get; set; }
    }
}
