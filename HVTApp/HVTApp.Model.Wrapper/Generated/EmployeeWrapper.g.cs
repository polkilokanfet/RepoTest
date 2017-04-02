using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class EmployeeWrapper : WrapperBase<Employee>
  {
    protected EmployeeWrapper(Employee model) : base(model) { }
    //public EmployeeWrapper(Employee model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static EmployeeWrapper GetWrapper(Employee model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (EmployeeWrapper)Repository.ModelWrapperDictionary[model];

		return new EmployeeWrapper(model);
	}



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

	private CompanyWrapper _fieldCompany;
	public CompanyWrapper Company 
    {
        get { return _fieldCompany; }
        set
        {
            if (Equals(_fieldCompany, value))
                return;

            UnRegisterComplexProperty(_fieldCompany);

            _fieldCompany = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private EmployeesPositionWrapper _fieldPosition;
	public EmployeesPositionWrapper Position 
    {
        get { return _fieldPosition; }
        set
        {
            if (Equals(_fieldPosition, value))
                return;

            UnRegisterComplexProperty(_fieldPosition);

            _fieldPosition = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(Employee model)
    {

        Company = CompanyWrapper.GetWrapper(model.Company);

        Position = EmployeesPositionWrapper.GetWrapper(model.Position);

    }

  }
}
