using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class EmployeeWrapper : WrapperBase<Employee>
  {
    public EmployeeWrapper() : base(new Employee()) { }
    public EmployeeWrapper(Employee model) : base(model) { }

//	public static EmployeeWrapper GetWrapper()
//	{
//		return GetWrapper(new Employee());
//	}
//
//	public static EmployeeWrapper GetWrapper(Employee model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (EmployeeWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new EmployeeWrapper(model);
//	}


    #region SimpleProperties
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

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private PersonWrapper _fieldPerson;
	public PersonWrapper Person 
    {
        get { return _fieldPerson; }
        set
        {
			SetComplexProperty<PersonWrapper, Person>(_fieldPerson, value);
			_fieldPerson = value;
        }
    }
    public PersonWrapper PersonOriginalValue { get; private set; }
    public bool PersonIsChanged => GetIsChanged(nameof(Person));

	private CompanyWrapper _fieldCompany;
	public CompanyWrapper Company 
    {
        get { return _fieldCompany; }
        set
        {
			SetComplexProperty<CompanyWrapper, Company>(_fieldCompany, value);
			_fieldCompany = value;
        }
    }
    public CompanyWrapper CompanyOriginalValue { get; private set; }
    public bool CompanyIsChanged => GetIsChanged(nameof(Company));

	private EmployeesPositionWrapper _fieldPosition;
	public EmployeesPositionWrapper Position 
    {
        get { return _fieldPosition; }
        set
        {
			SetComplexProperty<EmployeesPositionWrapper, EmployeesPosition>(_fieldPosition, value);
			_fieldPosition = value;
        }
    }
    public EmployeesPositionWrapper PositionOriginalValue { get; private set; }
    public bool PositionIsChanged => GetIsChanged(nameof(Position));

    #endregion
    protected override void InitializeComplexProperties(Employee model)
    {
        Person = GetWrapper<PersonWrapper, Person>(model.Person);
        Company = GetWrapper<CompanyWrapper, Company>(model.Company);
        Position = GetWrapper<EmployeesPositionWrapper, EmployeesPosition>(model.Position);
    }
  }
}
