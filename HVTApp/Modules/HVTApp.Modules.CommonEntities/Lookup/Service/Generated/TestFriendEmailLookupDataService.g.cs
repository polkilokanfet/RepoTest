using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class TestFriendEmailLookupDataService : LookupDataService<TestFriendEmailLookup, TestFriendEmail>, ITestFriendEmailLookupDataService
    {
        public TestFriendEmailLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
