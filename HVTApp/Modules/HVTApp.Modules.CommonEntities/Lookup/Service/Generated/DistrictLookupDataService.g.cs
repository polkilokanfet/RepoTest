using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class DistrictLookupDataService : LookupDataService<DistrictLookup, District>, IDistrictLookupDataService
    {
        public DistrictLookupDataService(HvtAppContext context) : base(context) { }
    }
}
