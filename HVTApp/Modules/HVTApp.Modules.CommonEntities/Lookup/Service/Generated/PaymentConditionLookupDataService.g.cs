using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentConditionLookupDataService : LookupDataService<PaymentConditionLookup, PaymentCondition>, IPaymentConditionLookupDataService
    {
        public PaymentConditionLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}