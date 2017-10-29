using System;
using HVTApp.DataAccess;
using HVTApp.Model.POCOs;

namespace HVTApp.UI.Wrapper
{
    public partial class UserWrapperDataService : WrapperDataService<User, UserWrapper>
    {
        public UserWrapperDataService(HvtAppContext context) : base(context)
        {
        }
		
		protected override UserWrapper GenerateWrapper(User model)
        {
            return new UserWrapper(model);
        }
    }
}
