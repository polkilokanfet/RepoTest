using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CompanyFormWrapperDataService : WrapperDataService<CompanyForm, CompanyFormWrapper>
    {
        public CompanyFormWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override CompanyFormWrapper GenerateWrapper(CompanyForm model)
        {
            return new CompanyFormWrapper(model);
        }
    }
}
