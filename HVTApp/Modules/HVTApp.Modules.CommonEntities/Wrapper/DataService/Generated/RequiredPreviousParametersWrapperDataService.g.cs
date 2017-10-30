using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class RequiredPreviousParametersWrapperDataService : WrapperDataService<RequiredPreviousParameters, RequiredPreviousParametersWrapper>
    {
        public RequiredPreviousParametersWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override RequiredPreviousParametersWrapper GenerateWrapper(RequiredPreviousParameters model)
        {
            return new RequiredPreviousParametersWrapper(model);
        }
    }
}
