using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class UserRoleLookupDataService : LookupDataService<UserRoleLookup, UserRole>, IUserRoleLookupDataService
    {
        public UserRoleLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
