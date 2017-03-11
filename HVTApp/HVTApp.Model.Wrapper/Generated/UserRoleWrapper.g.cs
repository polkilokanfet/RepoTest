using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class UserRoleWrapper : WrapperBase<UserRole>
  {
    public UserRoleWrapper(UserRole model) : base(model) { }
    public UserRoleWrapper(UserRole model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

    #region ComplexProperties
    public UserWrapper User { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(UserRole model)
    {
      if (model.User == null) throw new ArgumentException("User cannot be null");
      if (ExistsWrappers.ContainsKey(model.User))
      {
          User = (UserWrapper)ExistsWrappers[model.User];
      }
      else
      {
          User = new UserWrapper(model.User, ExistsWrappers);
          RegisterComplexProperty(User);
      }

    }
  }
}
