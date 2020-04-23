using System;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
    public class SalesUnitPaymentsPlannedWrapper : WrapperBase<SalesUnit>
    {
        public SalesUnitPaymentsPlannedWrapper(SalesUnit model) : base(model) { }

        public IValidatableChangeTrackingCollection<PaymentPlannedWrapper> PaymentsPlanned { get; private set; }

        protected override void InitializeCollectionProperties()
        {
            if (Model.PaymentsPlanned == null) throw new ArgumentException($"{nameof(PaymentsPlanned)} cannot be null");
            PaymentsPlanned = new ValidatableChangeTrackingCollection<PaymentPlannedWrapper>(Model.PaymentsPlanned.Select(e => new PaymentPlannedWrapper(e)));
            RegisterCollection(PaymentsPlanned, Model.PaymentsPlanned);
        }
    }
}