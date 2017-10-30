using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class AddressWrapperDataService : WrapperDataService<Address, AddressWrapper>
    {
        public AddressWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override AddressWrapper GenerateWrapper(Address model)
        {
            return new AddressWrapper(model);
        }
    }
}
