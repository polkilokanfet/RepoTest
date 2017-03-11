using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class UserWrapper : WrapperBase<User>
  {
    public UserWrapper(User model) : base(model) { }
    public UserWrapper(User model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String Login
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String LoginOriginalValue => GetOriginalValue<System.String>(nameof(Login));
    public bool LoginIsChanged => GetIsChanged(nameof(Login));

    public System.Guid Password
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid PasswordOriginalValue => GetOriginalValue<System.Guid>(nameof(Password));
    public bool PasswordIsChanged => GetIsChanged(nameof(Password));

    public System.String PersonalNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String PersonalNumberOriginalValue => GetOriginalValue<System.String>(nameof(PersonalNumber));
    public bool PersonalNumberIsChanged => GetIsChanged(nameof(PersonalNumber));

    public HVTApp.Model.Role RoleCurrent
    {
      get { return GetValue<HVTApp.Model.Role>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.Role RoleCurrentOriginalValue => GetOriginalValue<HVTApp.Model.Role>(nameof(RoleCurrent));
    public bool RoleCurrentIsChanged => GetIsChanged(nameof(RoleCurrent));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public EmployeeWrapper Employee { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<UserRoleWrapper> Roles { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(User model)
    {
      if (model.Employee == null) throw new ArgumentException("Employee cannot be null");
      if (ExistsWrappers.ContainsKey(model.Employee))
      {
          Employee = (EmployeeWrapper)ExistsWrappers[model.Employee];
      }
      else
      {
          Employee = new EmployeeWrapper(model.Employee, ExistsWrappers);
          RegisterComplexProperty(Employee);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(User model)
    {
      if (model.Roles == null) throw new ArgumentException("Roles cannot be null");
      Roles = new ValidatableChangeTrackingCollection<UserRoleWrapper>(model.Roles.Select(e => new UserRoleWrapper(e, ExistsWrappers)));
      RegisterCollection(Roles, model.Roles);

    }
  }
}
