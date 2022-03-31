using HVTApp.Model.POCOs;
using HVTApp.Model.Wrapper.Base;

namespace HVTApp.Model.Wrapper
{
    public class UserEmptyWrapper : WrapperBase<User>
    {
        public UserEmptyWrapper(User model) : base(model)
        {
        }
    }
}