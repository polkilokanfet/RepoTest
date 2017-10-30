using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CostWrapperDataService : WrapperDataService<Cost, CostWrapper>
    {
        public CostWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CostWrapper GenerateWrapper(Cost model)
        {
            return new CostWrapper(model);
        }
    }
}
