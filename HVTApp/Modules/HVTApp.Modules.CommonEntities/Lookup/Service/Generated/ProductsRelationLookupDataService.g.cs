using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Lookup
{
    public partial class ProductsRelationLookupDataService : LookupDataService<ProductsRelationLookup, ProductsRelation>, IProductsRelationLookupDataService
    {
        public ProductsRelationLookupDataService(HvtAppContext context) : base(context) { }
    }
}
