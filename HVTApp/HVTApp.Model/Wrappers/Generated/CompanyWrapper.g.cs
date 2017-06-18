using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    private CompanyWrapper() : base(new Company()) { }
    private CompanyWrapper(Company model) : base(model) { }



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

	public CompanyFormWrapper Form 
    {
        get { return GetComplexProperty<CompanyFormWrapper, CompanyForm>(Model.Form); }
        set { SetComplexProperty<CompanyFormWrapper, CompanyForm>(Form, value); }
    }

    public CompanyFormWrapper FormOriginalValue { get; private set; }
    public bool FormIsChanged => GetIsChanged(nameof(Form));


	public CompanyWrapper ParentCompany 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.ParentCompany); }
        set { SetComplexProperty<CompanyWrapper, Company>(ParentCompany, value); }
    }

    public CompanyWrapper ParentCompanyOriginalValue { get; private set; }
    public bool ParentCompanyIsChanged => GetIsChanged(nameof(ParentCompany));


	public AddressWrapper Address 
    {
        get { return GetComplexProperty<AddressWrapper, Address>(Model.Address); }
        set { SetComplexProperty<AddressWrapper, Address>(Address, value); }
    }

    public AddressWrapper AddressOriginalValue { get; private set; }
    public bool AddressIsChanged => GetIsChanged(nameof(Address));


	public BankDetailsWrapper BankDetails 
    {
        get { return GetComplexProperty<BankDetailsWrapper, BankDetails>(Model.BankDetails); }
        set { SetComplexProperty<BankDetailsWrapper, BankDetails>(BankDetails, value); }
    }

    public BankDetailsWrapper BankDetailsOriginalValue { get; private set; }
    public bool BankDetailsIsChanged => GetIsChanged(nameof(BankDetails));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<CompanyWrapper> ChildCompanies { get; private set; }


    public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }


    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Form = GetWrapper<CompanyFormWrapper, CompanyForm>(Model.Form);

        ParentCompany = GetWrapper<CompanyWrapper, Company>(Model.ParentCompany);

        Address = GetWrapper<AddressWrapper, Address>(Model.Address);

        BankDetails = GetWrapper<BankDetailsWrapper, BankDetails>(Model.BankDetails);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.ChildCompanies == null) throw new ArgumentException("ChildCompanies cannot be null");
      ChildCompanies = new ValidatableChangeTrackingCollection<CompanyWrapper>(Model.ChildCompanies.Select(e => GetWrapper<CompanyWrapper, Company>(e)));
      RegisterCollection(ChildCompanies, Model.ChildCompanies);


      if (Model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
      ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(Model.ActivityFilds.Select(e => GetWrapper<ActivityFieldWrapper, ActivityField>(e)));
      RegisterCollection(ActivityFilds, Model.ActivityFilds);


      if (Model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(Model.Employees.Select(e => GetWrapper<EmployeeWrapper, Employee>(e)));
      RegisterCollection(Employees, Model.Employees);


    }

  }
}
