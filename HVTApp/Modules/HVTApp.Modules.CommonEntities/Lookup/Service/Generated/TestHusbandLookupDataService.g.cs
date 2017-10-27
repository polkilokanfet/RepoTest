using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class TestHusbandLookupDataService : LookupDataService<TestHusbandLookup, TestHusband>, ITestHusbandLookupDataService
    {
        public TestHusbandLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
