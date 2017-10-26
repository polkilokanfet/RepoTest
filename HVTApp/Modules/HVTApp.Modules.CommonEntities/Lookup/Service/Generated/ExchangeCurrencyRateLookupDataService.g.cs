using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class ExchangeCurrencyRateLookupDataService : LookupDataService<ExchangeCurrencyRateLookup, ExchangeCurrencyRate>, IExchangeCurrencyRateLookupDataService
    {
        public ExchangeCurrencyRateLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
