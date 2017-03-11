using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    public CompanyWrapper(Company model) : base(model) { }
    public CompanyWrapper(Company model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

    #region SimpleProperties
    public System.String FullName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String FullNameOriginalValue => GetOriginalValue<System.String>(nameof(FullName));
    public bool FullNameIsChanged => GetIsChanged(nameof(FullName));

    public System.String ShortName
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String ShortNameOriginalValue => GetOriginalValue<System.String>(nameof(ShortName));
    public bool ShortNameIsChanged => GetIsChanged(nameof(ShortName));

    public System.String Inn
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String InnOriginalValue => GetOriginalValue<System.String>(nameof(Inn));
    public bool InnIsChanged => GetIsChanged(nameof(Inn));

    public System.String Kpp
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String KppOriginalValue => GetOriginalValue<System.String>(nameof(Kpp));
    public bool KppIsChanged => GetIsChanged(nameof(Kpp));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
    public CompanyFormWrapper Form { get; private set; }

    public AddressWrapper Address { get; private set; }

    public BankDetailsWrapper BankDetails { get; private set; }

    #endregion

    #region CollectionComplexProperties
    public ValidatableChangeTrackingCollection<CompanyWrapper> ChildCompanies { get; private set; }

    public ValidatableChangeTrackingCollection<ActivityFildWrapper> ActivityFilds { get; private set; }

    public ValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }

    #endregion
    
    protected override void InitializeComplexProperties(Company model)
    {
      if (model.Form == null) throw new ArgumentException("Form cannot be null");
      if (ExistsWrappers.ContainsKey(model.Form))
      {
          Form = (CompanyFormWrapper)ExistsWrappers[model.Form];
      }
      else
      {
          Form = new CompanyFormWrapper(model.Form, ExistsWrappers);
          RegisterComplexProperty(Form);
      }

      if (model.Address == null) throw new ArgumentException("Address cannot be null");
      if (ExistsWrappers.ContainsKey(model.Address))
      {
          Address = (AddressWrapper)ExistsWrappers[model.Address];
      }
      else
      {
          Address = new AddressWrapper(model.Address, ExistsWrappers);
          RegisterComplexProperty(Address);
      }

      if (model.BankDetails == null) throw new ArgumentException("BankDetails cannot be null");
      if (ExistsWrappers.ContainsKey(model.BankDetails))
      {
          BankDetails = (BankDetailsWrapper)ExistsWrappers[model.BankDetails];
      }
      else
      {
          BankDetails = new BankDetailsWrapper(model.BankDetails, ExistsWrappers);
          RegisterComplexProperty(BankDetails);
      }

    }
  
    protected override void InitializeCollectionComplexProperties(Company model)
    {
      if (model.ChildCompanies == null) throw new ArgumentException("ChildCompanies cannot be null");
      ChildCompanies = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.ChildCompanies.Select(e => new CompanyWrapper(e, ExistsWrappers)));
      RegisterCollection(ChildCompanies, model.ChildCompanies);

      if (model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
      ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFildWrapper>(model.ActivityFilds.Select(e => new ActivityFildWrapper(e, ExistsWrappers)));
      RegisterCollection(ActivityFilds, model.ActivityFilds);

      if (model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.Employees.Select(e => new EmployeeWrapper(e, ExistsWrappers)));
      RegisterCollection(Employees, model.Employees);

    }
  }
}
