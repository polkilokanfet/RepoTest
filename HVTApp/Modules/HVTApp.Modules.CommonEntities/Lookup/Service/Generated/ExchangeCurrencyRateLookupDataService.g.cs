using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ExchangeCurrencyRateLookupDataService : LookupDataService<ExchangeCurrencyRateLookup, ExchangeCurrencyRate>, IExchangeCurrencyRateLookupDataService
    {
        public ExchangeCurrencyRateLookupDataService(HvtAppContext context) : base(context) { }
    }
}
