using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendGroupWrapperDataService : WrapperDataService<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override TestFriendGroupWrapper GenerateWrapper(TestFriendGroup model)
        {
            return new TestFriendGroupWrapper(model);
        }
    }
}
