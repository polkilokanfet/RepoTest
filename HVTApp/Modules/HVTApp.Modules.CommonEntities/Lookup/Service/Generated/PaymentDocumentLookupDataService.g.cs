using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentDocumentLookupDataService : LookupDataService<PaymentDocumentLookup, PaymentDocument>, IPaymentDocumentLookupDataService
    {
        public PaymentDocumentLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}