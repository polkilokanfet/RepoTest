using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CostLookupDataService : LookupDataService<CostLookup, Cost>, ICostLookupDataService
    {
        public CostLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}