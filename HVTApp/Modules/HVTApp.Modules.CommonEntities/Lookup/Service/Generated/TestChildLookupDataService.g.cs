using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TestChildLookupDataService : LookupDataService<TestChildLookup, TestChild>, ITestChildLookupDataService
    {
        public TestChildLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
