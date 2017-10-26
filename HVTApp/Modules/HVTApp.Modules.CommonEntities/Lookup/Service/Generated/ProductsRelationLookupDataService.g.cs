using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public class ProductsRelationLookupDataService : LookupDataService<ProductsRelationLookup, ProductsRelation>, IProductsRelationLookupDataService
    {
        public ProductsRelationLookupDataService(Func<HvtAppContext> contextCreator) : base(contextCreator) { }
    }
}
