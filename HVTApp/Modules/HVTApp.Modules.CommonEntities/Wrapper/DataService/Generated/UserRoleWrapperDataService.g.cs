using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class UserRoleWrapperDataService : WrapperDataService<UserRole, UserRoleWrapper>
    {
        public UserRoleWrapperDataService(Func<HvtAppContext> contextCreator) : base(contextCreator)
        {
        }
		
		protected override UserRoleWrapper GenerateWrapper(UserRole model)
        {
            return new UserRoleWrapper(model);
        }
    }
}
