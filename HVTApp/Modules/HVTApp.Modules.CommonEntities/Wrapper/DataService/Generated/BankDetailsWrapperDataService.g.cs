using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class BankDetailsWrapperDataService : WrapperDataService<BankDetails, BankDetailsWrapper>
    {
        public BankDetailsWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override BankDetailsWrapper GenerateWrapper(BankDetails model)
        {
            return new BankDetailsWrapper(model);
        }
    }
}
