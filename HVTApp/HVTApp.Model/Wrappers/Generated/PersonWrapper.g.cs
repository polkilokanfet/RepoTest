using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class PersonWrapper : WrapperBase<Person>
  {
    private PersonWrapper(IGetWrapper getWrapper) : base(new Person(), getWrapper) { }
    private PersonWrapper(Person model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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


    public System.Boolean IsMan
    {
      get { return GetValue<System.Boolean>(); }
      set { SetValue(value); }
    }
    public System.Boolean IsManOriginalValue => GetOriginalValue<System.Boolean>(nameof(IsMan));
    public bool IsManIsChanged => GetIsChanged(nameof(IsMan));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }


    #endregion

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.Employees.Select(e => GetWrapper<EmployeeWrapper, Employee>(e)));
      RegisterCollection(Employees, Model.Employees);


    }

  }
}
