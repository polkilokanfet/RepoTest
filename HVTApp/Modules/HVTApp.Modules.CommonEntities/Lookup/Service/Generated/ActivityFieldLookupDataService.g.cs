using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ActivityFieldLookupDataService : LookupDataService<ActivityFieldLookup, ActivityField>, IActivityFieldLookupDataService
    {
        public ActivityFieldLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
