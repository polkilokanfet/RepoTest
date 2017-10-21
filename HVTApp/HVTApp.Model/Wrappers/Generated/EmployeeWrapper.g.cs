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

	public PersonWrapper Person { get; set; }

	public CompanyWrapper Company { get; set; }

	public EmployeesPositionWrapper Position { get; set; }

    #endregion

    public override void InitializeComplexProperties()
    {

        Person = new PersonWrapper(Model.Person);
		RegisterComplex(Person);

        Company = new CompanyWrapper(Model.Company);
		RegisterComplex(Company);

        Position = new EmployeesPositionWrapper(Model.Position);
		RegisterComplex(Position);

    }

  }
}
