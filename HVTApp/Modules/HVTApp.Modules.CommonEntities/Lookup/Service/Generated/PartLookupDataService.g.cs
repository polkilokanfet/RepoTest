using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class PartLookupDataService : LookupDataService<PartLookup, Part>, IPartLookupDataService
    {
        public PartLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}