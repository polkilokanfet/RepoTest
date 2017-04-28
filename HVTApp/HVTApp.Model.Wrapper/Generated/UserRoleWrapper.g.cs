using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class UserRoleWrapper : WrapperBase<UserRole>
  {
    public UserRoleWrapper() : base(new UserRole(), new Dictionary<IBaseEntity, object>()) { }
    public UserRoleWrapper(UserRole model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public UserRoleWrapper(UserRole model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public UserRoleWrapper(UserRole model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


    #region SimpleProperties
    public HVTApp.Model.Role Role
    {
      get { return GetValue<HVTApp.Model.Role>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.Role RoleOriginalValue => GetOriginalValue<HVTApp.Model.Role>(nameof(Role));
    public bool RoleIsChanged => GetIsChanged(nameof(Role));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion
  }
}
