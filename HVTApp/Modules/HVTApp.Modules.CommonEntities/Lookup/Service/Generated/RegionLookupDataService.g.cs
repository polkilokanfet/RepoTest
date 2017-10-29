using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class RegionLookupDataService : LookupDataService<RegionLookup, Region>, IRegionLookupDataService
    {
        public RegionLookupDataService(HvtAppContext context) : base(context) { }
    }
}
