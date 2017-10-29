using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ContractWrapperDataService : WrapperDataService<Contract, ContractWrapper>
    {
        public ContractWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override ContractWrapper GenerateWrapper(Contract model)
        {
            return new ContractWrapper(model);
        }
    }
}
