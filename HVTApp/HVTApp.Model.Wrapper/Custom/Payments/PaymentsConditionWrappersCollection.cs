using System.Collections.Generic;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public class PaymentsConditionWrappersCollection : ValidatableChangeTrackingCollection<PaymentsConditionWrapper>
    {
        public PaymentsConditionWrappersCollection(IEnumerable<PaymentsConditionWrapper> items) : base(items)
        {
        }

        public override bool IsValid => TotalPart != 100 && base.IsValid;

        public double TotalPart => this.Select(x => x.PartInPercent).Sum();

        public new void Add(PaymentsConditionWrapper payment)
        {
            if (TotalPart >= 100)
                return;

            if ((TotalPart + payment.PartInPercent) > 100)
                payment.PartInPercent = 100 - TotalPart;

            base.Add(payment);
        }
    }

    public class PaymentsActualWrappersCollection : ValidatableChangeTrackingCollection<PaymentActualWrapper>
    {
        public PaymentsActualWrappersCollection(IEnumerable<PaymentActualWrapper> items) : base(items)
        {
        }
    }

    public class PaymentsPlannedWrappersCollection : ValidatableChangeTrackingCollection<PaymentPlannedWrapper>
    {
        public PaymentsPlannedWrappersCollection(IEnumerable<PaymentPlannedWrapper> items) : base(items)
        {
        }
    }
}