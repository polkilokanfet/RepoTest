using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class PersonWrapper : WrapperBase<Person>
  {
    protected PersonWrapper(Person model) : base(model) { }

	public static PersonWrapper GetWrapper()
	{
		return GetWrapper(new Person());
	}

	public static PersonWrapper GetWrapper(Person model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (PersonWrapper)Repository.ModelWrapperDictionary[model];

		return new PersonWrapper(model);
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


    public HVTApp.Model.Gender Gender
    {
      get { return GetValue<HVTApp.Model.Gender>(); }
      set { SetValue(value); }
    }
    public HVTApp.Model.Gender GenderOriginalValue => GetOriginalValue<HVTApp.Model.Gender>(nameof(Gender));
    public bool GenderIsChanged => GetIsChanged(nameof(Gender));


    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region ComplexProperties

	public EmployeeWrapper CurrentEmployee 
    {
        get { return EmployeeWrapper.GetWrapper(Model.CurrentEmployee); }
        set
        {
			var oldPropVal = CurrentEmployee;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public EmployeeWrapper CurrentEmployeeOriginalValue => EmployeeWrapper.GetWrapper(GetOriginalValue<Employee>(nameof(CurrentEmployee)));
    public bool CurrentEmployeeIsChanged => GetIsChanged(nameof(CurrentEmployee));


    #endregion

    protected override void InitializeComplexProperties(Person model)
    {

        CurrentEmployee = EmployeeWrapper.GetWrapper(model.CurrentEmployee);

    }

  }
}
