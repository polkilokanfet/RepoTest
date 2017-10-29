using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendEmailWrapperDataService : WrapperDataService<TestFriendEmail, TestFriendEmailWrapper>
    {
        public TestFriendEmailWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override TestFriendEmailWrapper GenerateWrapper(TestFriendEmail model)
        {
            return new TestFriendEmailWrapper(model);
        }
    }
}
