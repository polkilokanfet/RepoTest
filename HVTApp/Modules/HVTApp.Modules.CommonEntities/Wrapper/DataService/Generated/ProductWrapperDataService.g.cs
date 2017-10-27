using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductWrapperDataService : WrapperDataService<Product, ProductWrapper>
    {
        public ProductWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override ProductWrapper GenerateWrapper(Product model)
        {
            return new ProductWrapper(model);
        }
    }
}
