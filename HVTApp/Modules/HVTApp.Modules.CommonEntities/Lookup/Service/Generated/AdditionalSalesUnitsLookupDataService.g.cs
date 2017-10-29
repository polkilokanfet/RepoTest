using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class AdditionalSalesUnitsLookupDataService : LookupDataService<AdditionalSalesUnitsLookup, AdditionalSalesUnits>, IAdditionalSalesUnitsLookupDataService
    {
        public AdditionalSalesUnitsLookupDataService(HvtAppContext context) : base(context) { }
    }
}
