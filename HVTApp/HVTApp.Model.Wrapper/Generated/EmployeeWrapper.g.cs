using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class EmployeeWrapper : WrapperBase<Employee>
  {
    public EmployeeWrapper(Employee model) : base(model) { }
    public EmployeeWrapper(Employee model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


    #region SimpleProperties

    public System.String Surname
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String SurnameOriginalValue => GetOriginalValue<System.String>(nameof(Surname));
    public bool SurnameIsChanged => GetIsChanged(nameof(Surname));


    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.String Patronymic
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String PatronymicOriginalValue => GetOriginalValue<System.String>(nameof(Patronymic));
    public bool PatronymicIsChanged => GetIsChanged(nameof(Patronymic));


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

	public CompanyWrapper Company
	{
		get { return GetComplexProperty<Company, CompanyWrapper>(nameof(Company)); }
		set { SetComplexProperty<Company, CompanyWrapper>(value, this.Company, nameof(Company)); }
	}


	public EmployeesPositionWrapper Position
	{
		get { return GetComplexProperty<EmployeesPosition, EmployeesPositionWrapper>(nameof(Position)); }
		set { SetComplexProperty<EmployeesPosition, EmployeesPositionWrapper>(value, this.Position, nameof(Position)); }
	}


    #endregion

    
    protected override void InitializeComplexProperties(Employee model)
    {

		if (model.Company != null)
		{
			if (ExistsWrappers.ContainsKey(model.Company))
			{
				Company = (CompanyWrapper)ExistsWrappers[model.Company];
			}
			else
			{
				Company = new CompanyWrapper(model.Company, ExistsWrappers);
				//ExistsWrappers.Add(model.Company, new CompanyWrapper(model.Company, ExistsWrappers));
				RegisterComplexProperty(Company);
			}
		}


		if (model.Position != null)
		{
			if (ExistsWrappers.ContainsKey(model.Position))
			{
				Position = (EmployeesPositionWrapper)ExistsWrappers[model.Position];
			}
			else
			{
				Position = new EmployeesPositionWrapper(model.Position, ExistsWrappers);
				//ExistsWrappers.Add(model.Position, new EmployeesPositionWrapper(model.Position, ExistsWrappers));
				RegisterComplexProperty(Position);
			}
		}


    }

  }
}
