using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AddressWrapperDataService : WrapperDataService<Address, AddressWrapper>
    {
        public AddressWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override AddressWrapper GenerateWrapper(Address model)
        {
            return new AddressWrapper(model);
        }
    }
}
