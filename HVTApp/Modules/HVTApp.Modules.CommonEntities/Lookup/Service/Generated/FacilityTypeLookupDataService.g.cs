using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class FacilityTypeLookupDataService : LookupDataService<FacilityTypeLookup, FacilityType>, IFacilityTypeLookupDataService
    {
        public FacilityTypeLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
