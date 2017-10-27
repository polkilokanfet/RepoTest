using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class BankDetailsWrapperDataService : WrapperDataService<BankDetails, BankDetailsWrapper>
    {
        public BankDetailsWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override BankDetailsWrapper GenerateWrapper(BankDetails model)
        {
            return new BankDetailsWrapper(model);
        }
    }
}
