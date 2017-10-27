using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class UserLookupDataService : LookupDataService<UserLookup, User>, IUserLookupDataService
    {
        public UserLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
