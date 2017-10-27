using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentPlannedLookupDataService : LookupDataService<PaymentPlannedLookup, PaymentPlanned>, IPaymentPlannedLookupDataService
    {
        public PaymentPlannedLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
