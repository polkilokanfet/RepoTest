using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TestFriendGroupLookupDataService : LookupDataService<TestFriendGroupLookup, TestFriendGroup>, ITestFriendGroupLookupDataService
    {
        public TestFriendGroupLookupDataService(HvtAppContext context) : base(context) { }
    }
}
