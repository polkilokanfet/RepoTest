using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class EmployeeWrapper : WrapperBase<Employee>
	{
	public EmployeeWrapper(Employee model) : base(model) { }

	
    #region SimpleProperties
    public System.Guid PersonId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid PersonIdOriginalValue => GetOriginalValue<System.Guid>(nameof(PersonId));
    public bool PersonIdIsChanged => GetIsChanged(nameof(PersonId));

    public System.Boolean IsActual
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsActualOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsActual));
    public bool IsActualIsChanged => GetIsChanged(nameof(IsActual));

    public System.String PhoneNumber
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String PhoneNumberOriginalValue => GetOriginalValue<System.String>(nameof(PhoneNumber));
    public bool PhoneNumberIsChanged => GetIsChanged(nameof(PhoneNumber));

    public System.String Email
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String EmailOriginalValue => GetOriginalValue<System.String>(nameof(Email));
    public bool EmailIsChanged => GetIsChanged(nameof(Email));

    public System.Guid CompanyId
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid CompanyIdOriginalValue => GetOriginalValue<System.Guid>(nameof(CompanyId));
    public bool CompanyIdIsChanged => GetIsChanged(nameof(CompanyId));

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public EmployeesPositionWrapper Position 
    {
        get { return GetWrapper<EmployeesPositionWrapper>(); }
        set { SetComplexValue<EmployeesPosition, EmployeesPositionWrapper>(Position, value); }
    }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<EmployeesPositionWrapper>(nameof(Position), Model.Position == null ? null : new EmployeesPositionWrapper(Model.Position));

    }
	}
}
	