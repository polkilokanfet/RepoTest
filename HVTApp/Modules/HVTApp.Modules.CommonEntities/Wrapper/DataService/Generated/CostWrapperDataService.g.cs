using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CostWrapperDataService : WrapperDataService<Cost, CostWrapper>
    {
        public CostWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override CostWrapper GenerateWrapper(Cost model)
        {
            return new CostWrapper(model);
        }
    }
}
