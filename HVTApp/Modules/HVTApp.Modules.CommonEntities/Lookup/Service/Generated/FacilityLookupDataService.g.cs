using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class FacilityLookupDataService : LookupDataService<FacilityLookup, Facility>, IFacilityLookupDataService
    {
        public FacilityLookupDataService(HvtAppContext context) : base(context) { }
    }
}
