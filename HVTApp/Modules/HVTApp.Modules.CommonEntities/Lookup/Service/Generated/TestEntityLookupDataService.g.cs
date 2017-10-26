using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class TestEntityLookupDataService : LookupDataService<TestEntityLookup, TestEntity>, ITestEntityLookupDataService
    {
        public TestEntityLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
