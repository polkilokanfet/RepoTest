using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class LocalityLookupDataService : LookupDataService<LocalityLookup, Locality>, ILocalityLookupDataService
    {
        public LocalityLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}