using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class UserRoleLookupDataService : LookupDataService<UserRoleLookup, UserRole>, IUserRoleLookupDataService
    {
        public UserRoleLookupDataService(HvtAppContext context) : base(context) { }
    }
}
