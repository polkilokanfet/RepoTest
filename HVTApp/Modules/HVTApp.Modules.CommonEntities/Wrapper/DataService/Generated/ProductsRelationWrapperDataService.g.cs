using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductsRelationWrapperDataService : WrapperDataService<ProductsRelation, ProductsRelationWrapper>
    {
        public ProductsRelationWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override ProductsRelationWrapper GenerateWrapper(ProductsRelation model)
        {
            return new ProductsRelationWrapper(model);
        }
    }
}
