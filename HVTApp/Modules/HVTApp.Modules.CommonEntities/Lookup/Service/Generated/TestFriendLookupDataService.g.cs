using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class TestFriendLookupDataService : LookupDataService<TestFriendLookup, TestFriend>, ITestFriendLookupDataService
    {
        public TestFriendLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
