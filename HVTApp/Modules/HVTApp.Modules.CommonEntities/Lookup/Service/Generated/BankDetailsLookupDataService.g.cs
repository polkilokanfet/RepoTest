using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class BankDetailsLookupDataService : LookupDataService<BankDetailsLookup, BankDetails>, IBankDetailsLookupDataService
    {
        public BankDetailsLookupDataService(HvtAppContext context) : base(context) { }
    }
}
