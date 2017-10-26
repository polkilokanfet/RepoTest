using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class PaymentActualLookupDataService : LookupDataService<PaymentActualLookup, PaymentActual>, IPaymentActualLookupDataService
    {
        public PaymentActualLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
