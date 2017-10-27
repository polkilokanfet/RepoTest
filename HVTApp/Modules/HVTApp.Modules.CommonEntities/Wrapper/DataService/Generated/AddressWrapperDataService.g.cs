using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AddressWrapperDataService : WrapperDataService<Address, AddressWrapper>
    {
        public AddressWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override AddressWrapper GenerateWrapper(Address model)
        {
            return new AddressWrapper(model);
        }
    }
}
