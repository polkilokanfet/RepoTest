using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class CountryLookupDataService : LookupDataService<CountryLookup, Country>, ICountryLookupDataService
    {
        public CountryLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
