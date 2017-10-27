using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProductionUnitLookupDataService : LookupDataService<ProductionUnitLookup, ProductionUnit>, IProductionUnitLookupDataService
    {
        public ProductionUnitLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
