using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProductLookupDataService : LookupDataService<ProductLookup, Product>, IProductLookupDataService
    {
        public ProductLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
