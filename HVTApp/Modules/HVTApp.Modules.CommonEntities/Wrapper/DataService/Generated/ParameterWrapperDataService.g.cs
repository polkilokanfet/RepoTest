using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ParameterWrapperDataService : WrapperDataService<Parameter, ParameterWrapper>
    {
        public ParameterWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override ParameterWrapper GenerateWrapper(Parameter model)
        {
            return new ParameterWrapper(model);
        }
    }
}
