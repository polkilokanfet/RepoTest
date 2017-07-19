using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class UserRoleWrapper : WrapperBase<UserRole>
  {
    private UserRoleWrapper(IGetWrapper getWrapper) : base(new UserRole(), getWrapper) { }
    private UserRoleWrapper(UserRole model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public HVTApp.Model.POCOs.Role Role
    {
      get { return GetValue<HVTApp.Model.POCOs.Role>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.POCOs.Role RoleOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Role>(nameof(Role));
    public bool RoleIsChanged => GetIsChanged(nameof(Role));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion

  }
}
