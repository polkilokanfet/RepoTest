using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class UserRoleWrapper : WrapperBase<UserRole>
  {
    public UserRoleWrapper() : base(new UserRole()) { }
    public UserRoleWrapper(UserRole model) : base(model) { }

//	public static UserRoleWrapper GetWrapper()
//	{
//		return GetWrapper(new UserRole());
//	}
//
//	public static UserRoleWrapper GetWrapper(UserRole model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (UserRoleWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new UserRoleWrapper(model);
//	}


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
