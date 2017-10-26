using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class FacilityLookupDataService : LookupDataService<FacilityLookup, Facility>, IFacilityLookupDataService
    {
        public FacilityLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
