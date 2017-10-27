using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class BankDetailsLookupDataService : LookupDataService<BankDetailsLookup, BankDetails>, IBankDetailsLookupDataService
    {
        public BankDetailsLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
