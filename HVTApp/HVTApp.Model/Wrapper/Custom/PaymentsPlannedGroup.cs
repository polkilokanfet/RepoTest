using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base.TrackingCollections;

namespace HVTApp.Model.Wrapper
{
    public class PaymentsPlannedGroup
    {
        public IValidatableChangeTrackingCollection<SalesUnitPaymentsPlannedWrapper> SalesUnitPaymentsPlannedWrappers { get; }
        public SalesUnit SalesUnit => SalesUnitPaymentsPlannedWrappers.First().Model;
        public int Amount => SalesUnitPaymentsPlannedWrappers.Count;

        public PaymentsPlannedGroup(IEnumerable<SalesUnitPaymentsPlannedWrapper> salesUnitPaymentsPlannedWrappers)
        {
            SalesUnitPaymentsPlannedWrappers = new ValidatableChangeTrackingCollection<SalesUnitPaymentsPlannedWrapper>(salesUnitPaymentsPlannedWrappers);
        }
    }
}