using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentActualLookupDataService : LookupDataService<PaymentActualLookup, PaymentActual>, IPaymentActualLookupDataService
    {
        public PaymentActualLookupDataService(HvtAppContext context) : base(context) { }
    }
}
