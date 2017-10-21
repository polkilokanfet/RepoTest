using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class UserWrapper : WrapperBase<User>
  {
    public UserWrapper(User model) : base(model) { }



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


    public HVTApp.Model.POCOs.Role RoleCurrent
    {
      get { return GetValue<HVTApp.Model.POCOs.Role>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.POCOs.Role RoleCurrentOriginalValue => GetOriginalValue<HVTApp.Model.POCOs.Role>(nameof(RoleCurrent));
    public bool RoleCurrentIsChanged => GetIsChanged(nameof(RoleCurrent));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public EmployeeWrapper Employee { get; set; }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<UserRoleWrapper> Roles { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Employee = new EmployeeWrapper(Model.Employee);
		RegisterComplex(Employee);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Roles == null) throw new ArgumentException("Roles cannot be null");
      Roles = new ValidatableChangeTrackingCollection<UserRoleWrapper>(Model.Roles.Select(e => new UserRoleWrapper(e)));
      RegisterCollection(Roles, Model.Roles);


    }

  }
}
