using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendWrapperDataService : WrapperDataService<TestFriend, TestFriendWrapper>
    {
        public TestFriendWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TestFriendWrapper GenerateWrapper(TestFriend model)
        {
            return new TestFriendWrapper(model);
        }
    }
}
