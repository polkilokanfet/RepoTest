using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class CompanyWrapper : WrapperBase<Company>
	{
	public CompanyWrapper(Company model) : base(model) { }

	
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

    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	public CompanyFormWrapper Form 
    {
        get { return GetWrapper<CompanyFormWrapper>(); }
        set { SetComplexValue<CompanyForm, CompanyFormWrapper>(Form, value); }
    }

	public CompanyWrapper ParentCompany 
    {
        get { return GetWrapper<CompanyWrapper>(); }
        set { SetComplexValue<Company, CompanyWrapper>(ParentCompany, value); }
    }

	public AddressWrapper AddressLegal 
    {
        get { return GetWrapper<AddressWrapper>(); }
        set { SetComplexValue<Address, AddressWrapper>(AddressLegal, value); }
    }

	public AddressWrapper AddressPost 
    {
        get { return GetWrapper<AddressWrapper>(); }
        set { SetComplexValue<Address, AddressWrapper>(AddressPost, value); }
    }

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<BankDetailsWrapper> BankDetailsList { get; private set; }

    public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }

    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<CompanyFormWrapper>(nameof(Form), Model.Form == null ? null : new CompanyFormWrapper(Model.Form));

        InitializeComplexProperty<CompanyWrapper>(nameof(ParentCompany), Model.ParentCompany == null ? null : new CompanyWrapper(Model.ParentCompany));

        InitializeComplexProperty<AddressWrapper>(nameof(AddressLegal), Model.AddressLegal == null ? null : new AddressWrapper(Model.AddressLegal));

        InitializeComplexProperty<AddressWrapper>(nameof(AddressPost), Model.AddressPost == null ? null : new AddressWrapper(Model.AddressPost));

    }
  
    protected override void InitializeCollectionProperties()
    {
      if (Model.BankDetailsList == null) throw new ArgumentException("BankDetailsList cannot be null");
      BankDetailsList = new ValidatableChangeTrackingCollection<BankDetailsWrapper>(Model.BankDetailsList.Select(e => new BankDetailsWrapper(e)));
      RegisterCollection(BankDetailsList, Model.BankDetailsList);

      if (Model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
      ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(Model.ActivityFilds.Select(e => new ActivityFieldWrapper(e)));
      RegisterCollection(ActivityFilds, Model.ActivityFilds);

      if (Model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.Employees.Select(e => new EmployeeWrapper(e)));
      RegisterCollection(Employees, Model.Employees);

    }
	}
}
	