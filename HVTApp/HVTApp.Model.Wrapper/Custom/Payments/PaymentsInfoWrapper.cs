using System;
using System.Collections.Specialized;
using System.Linq;

namespace HVTApp.Model.Wrapper
{
    public partial class PaymentsInfoWrapper
    {
        protected override void RunInConstructor()
        {
            PaymentsConditions = new PaymentsConditionWrappersCollection(Model.PaymentsConditions.Select(PaymentsConditionWrapper.GetWrapper));
            PaymentsPlanned = new PaymentsPlannedWrappersCollection(Model.PaymentsPlanned.Select(PaymentPlannedWrapper.GetWrapper));
            PaymentsActual = new PaymentsActualWrappersCollection(Model.PaymentsActual.Select(PaymentActualWrapper.GetWrapper));
        }
    }
}
