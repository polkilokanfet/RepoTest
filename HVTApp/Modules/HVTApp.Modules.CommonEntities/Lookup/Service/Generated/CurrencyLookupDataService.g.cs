using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CurrencyLookupDataService : LookupDataService<CurrencyLookup, Currency>, ICurrencyLookupDataService
    {
        public CurrencyLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}