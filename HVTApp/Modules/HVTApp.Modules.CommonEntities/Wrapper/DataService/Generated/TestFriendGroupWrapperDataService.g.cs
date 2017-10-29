using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendGroupWrapperDataService : WrapperDataService<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TestFriendGroupWrapper GenerateWrapper(TestFriendGroup model)
        {
            return new TestFriendGroupWrapper(model);
        }
    }
}
