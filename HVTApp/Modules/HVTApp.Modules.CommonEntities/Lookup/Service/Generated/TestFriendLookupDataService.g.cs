using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TestFriendLookupDataService : LookupDataService<TestFriendLookup, TestFriend>, ITestFriendLookupDataService
    {
        public TestFriendLookupDataService(HvtAppContext context) : base(context) { }
    }
}
