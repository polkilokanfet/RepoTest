using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class UserWrapper : WrapperBase<User>
  {
    protected UserWrapper(User model) : base(model) { }

	public static UserWrapper GetWrapper(User model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (UserWrapper)Repository.ModelWrapperDictionary[model];

		return new UserWrapper(model);
	}



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

	public EmployeeWrapper Employee 
    {
        get { return EmployeeWrapper.GetWrapper(Model.Employee); }
        set
        {
            UnRegisterComplexProperty(Employee);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

    public ValidatableChangeTrackingCollection<UserRoleWrapper> Roles { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(User model)
    {

        Employee = EmployeeWrapper.GetWrapper(model.Employee);

    }

  
    protected override void InitializeCollectionComplexProperties(User model)
    {

      if (model.Roles == null) throw new ArgumentException("Roles cannot be null");
      Roles = new ValidatableChangeTrackingCollection<UserRoleWrapper>(model.Roles.Select(e => UserRoleWrapper.GetWrapper(e)));
      RegisterCollection(Roles, model.Roles);


    }

  }
}
