using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendAddressWrapperDataService : WrapperDataService<TestFriendAddress, TestFriendAddressWrapper>
    {
        public TestFriendAddressWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override TestFriendAddressWrapper GenerateWrapper(TestFriendAddress model)
        {
            return new TestFriendAddressWrapper(model);
        }
    }
}