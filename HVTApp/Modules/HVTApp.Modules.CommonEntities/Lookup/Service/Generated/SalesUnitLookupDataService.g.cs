using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class SalesUnitLookupDataService : LookupDataService<SalesUnitLookup, SalesUnit>, ISalesUnitLookupDataService
    {
        public SalesUnitLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}