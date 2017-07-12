using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class UserRoleWrapper : WrapperBase<UserRole>
  {
    private UserRoleWrapper() : base(new UserRole()) { }
    private UserRoleWrapper(UserRole model) : base(model) { }



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
