using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class SalesUnitPaymentWrapper : WrapperBase<SalesUnit>
    {
        public IValidatableChangeTrackingCollection<PaymentActualWrapper1> PaymentsActual { get; private set; }

        public SalesUnitPaymentWrapper(SalesUnit model) : base(model) { }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
            PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper1>(Model.PaymentsActual.Select(paymentActual => new PaymentActualWrapper1(paymentActual)));
            RegisterCollection(PaymentsActual, Model.PaymentsActual);
        }
    }
}