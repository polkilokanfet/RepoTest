using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class CompanyWrapperDataService : WrapperDataService<Company, CompanyWrapper>
    {
        public CompanyWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override CompanyWrapper GenerateWrapper(Company model)
        {
            return new CompanyWrapper(model);
        }
    }
}
