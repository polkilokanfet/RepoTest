using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class ContractWrapperDataService : WrapperDataService<Contract, ContractWrapper>
    {
        public ContractWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override ContractWrapper GenerateWrapper(Contract model)
        {
            return new ContractWrapper(model);
        }
    }
}
