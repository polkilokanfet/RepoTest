using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CompanyWrapperDataService : WrapperDataService<Company, CompanyWrapper>
    {
        public CompanyWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override CompanyWrapper GenerateWrapper(Company model)
        {
            return new CompanyWrapper(model);
        }
    }
}
