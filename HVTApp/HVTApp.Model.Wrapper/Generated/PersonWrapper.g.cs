using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class PersonWrapper : WrapperBase<Person>
  {
    public PersonWrapper() : base(new Person(), new Dictionary<IBaseEntity, object>()) { }
    public PersonWrapper(Person model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public PersonWrapper(Person model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public PersonWrapper(Person model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
        get { return GetComplexProperty<EmployeeWrapper, Employee>(Model.CurrentEmployee); }
        set { SetComplexProperty<EmployeeWrapper, Employee>(CurrentEmployee, value); }
    }

    public EmployeeWrapper CurrentEmployeeOriginalValue { get; private set; }
    public bool CurrentEmployeeIsChanged => GetIsChanged(nameof(CurrentEmployee));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Person model)
    {
        CurrentEmployee = GetWrapper<EmployeeWrapper, Employee>(model.CurrentEmployee);
    }
  
    protected override void InitializeCollectionComplexProperties(Person model)
    {
      if (model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.Employees.Select(e => GetWrapper<EmployeeWrapper, Employee>(e)));
      RegisterCollection(Employees, model.Employees);

    }
  }
}
