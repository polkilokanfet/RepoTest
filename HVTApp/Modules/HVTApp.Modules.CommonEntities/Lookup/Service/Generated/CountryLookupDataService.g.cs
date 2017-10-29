using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CountryLookupDataService : LookupDataService<CountryLookup, Country>, ICountryLookupDataService
    {
        public CountryLookupDataService(HvtAppContext context) : base(context) { }
    }
}
