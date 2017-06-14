using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class EmployeeWrapper : WrapperBase<Employee>
  {
    public EmployeeWrapper() : base(new Employee()) { }
    public EmployeeWrapper(Employee model) : base(model) { }



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
        get { return GetComplexProperty<PersonWrapper, Person>(Model.Person); }
        set { SetComplexProperty<PersonWrapper, Person>(Person, value); }
    }

    public PersonWrapper PersonOriginalValue { get; private set; }
    public bool PersonIsChanged => GetIsChanged(nameof(Person));


	public CompanyWrapper Company 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.Company); }
        set { SetComplexProperty<CompanyWrapper, Company>(Company, value); }
    }

    public CompanyWrapper CompanyOriginalValue { get; private set; }
    public bool CompanyIsChanged => GetIsChanged(nameof(Company));


	public EmployeesPositionWrapper Position 
    {
        get { return GetComplexProperty<EmployeesPositionWrapper, EmployeesPosition>(Model.Position); }
        set { SetComplexProperty<EmployeesPositionWrapper, EmployeesPosition>(Position, value); }
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
