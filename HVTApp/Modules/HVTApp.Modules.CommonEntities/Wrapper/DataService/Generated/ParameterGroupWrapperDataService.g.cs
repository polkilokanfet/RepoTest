using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ParameterGroupWrapperDataService : WrapperDataService<ParameterGroup, ParameterGroupWrapper>
    {
        public ParameterGroupWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override ParameterGroupWrapper GenerateWrapper(ParameterGroup model)
        {
            return new ParameterGroupWrapper(model);
        }
    }
}
