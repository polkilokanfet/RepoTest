using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class AdditionalSalesUnitsLookupDataService : LookupDataService<AdditionalSalesUnitsLookup, AdditionalSalesUnits>, IAdditionalSalesUnitsLookupDataService
    {
        public AdditionalSalesUnitsLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
