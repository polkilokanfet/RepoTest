using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    private CompanyWrapper(IGetWrapper getWrapper) : base(new Company(), getWrapper) { }
    private CompanyWrapper(Company model, IGetWrapper getWrapper) : base(model, getWrapper) { }



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


	public AddressWrapper AddressLegal 
    {
        get { return GetComplexProperty<AddressWrapper, Address>(Model.AddressLegal); }
        set { SetComplexProperty<AddressWrapper, Address>(AddressLegal, value); }
    }

    public AddressWrapper AddressLegalOriginalValue { get; private set; }
    public bool AddressLegalIsChanged => GetIsChanged(nameof(AddressLegal));


	public AddressWrapper AddressPost 
    {
        get { return GetComplexProperty<AddressWrapper, Address>(Model.AddressPost); }
        set { SetComplexProperty<AddressWrapper, Address>(AddressPost, value); }
    }

    public AddressWrapper AddressPostOriginalValue { get; private set; }
    public bool AddressPostIsChanged => GetIsChanged(nameof(AddressPost));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<BankDetailsWrapper> BankDetailsList { get; private set; }


    public IValidatableChangeTrackingCollection<CompanyWrapper> ChildCompanies { get; private set; }


    public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }


    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

        Form = GetWrapper<CompanyFormWrapper, CompanyForm>(Model.Form);

        ParentCompany = GetWrapper<CompanyWrapper, Company>(Model.ParentCompany);

        AddressLegal = GetWrapper<AddressWrapper, Address>(Model.AddressLegal);

        AddressPost = GetWrapper<AddressWrapper, Address>(Model.AddressPost);

    }

  
    protected override void InitializeCollectionComplexProperties()
    {

      if (Model.BankDetailsList == null) throw new ArgumentException("BankDetailsList cannot be null");
      BankDetailsList = new ValidatableChangeTrackingCollection<BankDetailsWrapper>(Model.BankDetailsList.Select(e => GetWrapper<BankDetailsWrapper, BankDetails>(e)));
      RegisterCollection(BankDetailsList, Model.BankDetailsList);


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
