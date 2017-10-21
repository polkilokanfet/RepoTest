using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class EmployeeWrapper : WrapperBase<Employee>
  {
    public EmployeeWrapper(Employee model) : base(model) { }



    #region SimpleProperties

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


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	private PersonWrapper _fieldPerson;
	public PersonWrapper Person 
    {
        get { return _fieldPerson ; }
        set
        {
            SetComplexValue<Person, PersonWrapper>(_fieldPerson, value);
            _fieldPerson  = value;
        }
    }

	private CompanyWrapper _fieldCompany;
	public CompanyWrapper Company 
    {
        get { return _fieldCompany ; }
        set
        {
            SetComplexValue<Company, CompanyWrapper>(_fieldCompany, value);
            _fieldCompany  = value;
        }
    }

	private EmployeesPositionWrapper _fieldPosition;
	public EmployeesPositionWrapper Position 
    {
        get { return _fieldPosition ; }
        set
        {
            SetComplexValue<EmployeesPosition, EmployeesPositionWrapper>(_fieldPosition, value);
            _fieldPosition  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Person != null)
        {
            _fieldPerson = new PersonWrapper(Model.Person);
            RegisterComplex(Person);
        }

		if (Model.Company != null)
        {
            _fieldCompany = new CompanyWrapper(Model.Company);
            RegisterComplex(Company);
        }

		if (Model.Position != null)
        {
            _fieldPosition = new EmployeesPositionWrapper(Model.Position);
            RegisterComplex(Position);
        }

    }

  }
}
