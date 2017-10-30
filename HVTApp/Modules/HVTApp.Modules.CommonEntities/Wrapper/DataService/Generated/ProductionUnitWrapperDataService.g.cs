using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ProductionUnitWrapperDataService : WrapperDataService<ProductionUnit, ProductionUnitWrapper>
    {
        public ProductionUnitWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ProductionUnitWrapper GenerateWrapper(ProductionUnit model)
        {
            return new ProductionUnitWrapper(model);
        }
    }
}
