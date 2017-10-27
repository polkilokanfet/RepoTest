using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class LocalityTypeLookupDataService : LookupDataService<LocalityTypeLookup, LocalityType>, ILocalityTypeLookupDataService
    {
        public LocalityTypeLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
