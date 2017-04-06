using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class EmployeeWrapper : WrapperBase<Employee>
  {
    protected EmployeeWrapper(Employee model) : base(model) { }

	public static EmployeeWrapper GetWrapper()
	{
		return GetWrapper(new Employee());
	}

	public static EmployeeWrapper GetWrapper(Employee model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (EmployeeWrapper)Repository.ModelWrapperDictionary[model];

		return new EmployeeWrapper(model);
	}



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

	public PersonWrapper Person 
    {
        get { return PersonWrapper.GetWrapper(Model.Person); }
        set
        {
			var oldPropVal = Person;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public PersonWrapper PersonOriginalValue => PersonWrapper.GetWrapper(GetOriginalValue<Person>(nameof(Person)));
    public bool PersonIsChanged => GetIsChanged(nameof(Person));


	public CompanyWrapper Company 
    {
        get { return CompanyWrapper.GetWrapper(Model.Company); }
        set
        {
			var oldPropVal = Company;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CompanyWrapper CompanyOriginalValue => CompanyWrapper.GetWrapper(GetOriginalValue<Company>(nameof(Company)));
    public bool CompanyIsChanged => GetIsChanged(nameof(Company));


	public EmployeesPositionWrapper Position 
    {
        get { return EmployeesPositionWrapper.GetWrapper(Model.Position); }
        set
        {
			var oldPropVal = Position;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EmployeesPositionWrapper PositionOriginalValue => EmployeesPositionWrapper.GetWrapper(GetOriginalValue<EmployeesPosition>(nameof(Position)));
    public bool PositionIsChanged => GetIsChanged(nameof(Position));


    #endregion

    protected override void InitializeComplexProperties(Employee model)
    {

        Person = PersonWrapper.GetWrapper(model.Person);

        Company = CompanyWrapper.GetWrapper(model.Company);

        Position = EmployeesPositionWrapper.GetWrapper(model.Position);

    }

  }
}
