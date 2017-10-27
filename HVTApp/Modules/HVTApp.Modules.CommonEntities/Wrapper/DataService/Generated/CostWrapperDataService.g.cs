using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CostWrapperDataService : WrapperDataService<Cost, CostWrapper>
    {
        public CostWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override CostWrapper GenerateWrapper(Cost model)
        {
            return new CostWrapper(model);
        }
    }
}
