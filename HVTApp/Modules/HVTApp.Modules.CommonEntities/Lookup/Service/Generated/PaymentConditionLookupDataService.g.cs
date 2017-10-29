using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PaymentConditionLookupDataService : LookupDataService<PaymentConditionLookup, PaymentCondition>, IPaymentConditionLookupDataService
    {
        public PaymentConditionLookupDataService(HvtAppContext context) : base(context) { }
    }
}
