using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.UI.Wrapper;

namespace HVTApp.UI.Modules.Sales.ViewModels
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