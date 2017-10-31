using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TestFriendAddressLookupDataService : LookupDataService<TestFriendAddressLookup, TestFriendAddress>, ITestFriendAddressLookupDataService
    {
        public TestFriendAddressLookupDataService(HvtAppContext context) : base(context) { }
    }
}
