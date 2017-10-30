using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class TestFriendGroupWrapperDataService : WrapperDataService<TestFriendGroup, TestFriendGroupWrapper>
    {
        public TestFriendGroupWrapperDataService(IUnitOfWork unitOfWork) : base(unitOfWork) { }
		
		protected override TestFriendGroupWrapper GenerateWrapper(TestFriendGroup model)
        {
            return new TestFriendGroupWrapper(model);
        }
    }
}
