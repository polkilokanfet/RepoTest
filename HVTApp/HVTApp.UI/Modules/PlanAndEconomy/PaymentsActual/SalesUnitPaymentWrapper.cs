using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.UI.Modules.PlanAndEconomy.PaymentsActual
{
    public class SalesUnitPaymentWrapper : WrapperBase<SalesUnit>
    {
        public SalesUnitPaymentWrapper(SalesUnit model) : base(model) { }

        public IValidatableChangeTrackingCollection<PaymentActualWrapper> PaymentsActual { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PaymentsActual == null) throw new ArgumentException("PaymentsActual cannot be null");
            PaymentsActual = new ValidatableChangeTrackingCollection<PaymentActualWrapper>(Model.PaymentsActual.Select(e => new PaymentActualWrapper(e)));
            RegisterCollection(PaymentsActual, Model.PaymentsActual);
        }
    }
}