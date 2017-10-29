using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TestWifeLookupDataService : LookupDataService<TestWifeLookup, TestWife>, ITestWifeLookupDataService
    {
        public TestWifeLookupDataService(HvtAppContext context) : base(context) { }
    }
}
