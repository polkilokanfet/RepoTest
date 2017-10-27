using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendWrapperDataService : WrapperDataService<TestFriend, TestFriendWrapper>
    {
        public TestFriendWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override TestFriendWrapper GenerateWrapper(TestFriend model)
        {
            return new TestFriendWrapper(model);
        }
    }
}
