using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class CostOnDateLookupDataService : LookupDataService<CostOnDateLookup, CostOnDate>, ICostOnDateLookupDataService
    {
        public CostOnDateLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}