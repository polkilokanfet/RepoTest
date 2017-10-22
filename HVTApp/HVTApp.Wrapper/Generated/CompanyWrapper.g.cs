using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Wrapper
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

	private CompanyFormWrapper _fieldForm;
	public CompanyFormWrapper Form 
    {
        get { return _fieldForm ; }
        set
        {
            SetComplexValue<CompanyForm, CompanyFormWrapper>(_fieldForm, value);
            _fieldForm  = value;
        }
    }

	private CompanyWrapper _fieldParentCompany;
	public CompanyWrapper ParentCompany 
    {
        get { return _fieldParentCompany ; }
        set
        {
            SetComplexValue<Company, CompanyWrapper>(_fieldParentCompany, value);
            _fieldParentCompany  = value;
        }
    }

	private AddressWrapper _fieldAddressLegal;
	public AddressWrapper AddressLegal 
    {
        get { return _fieldAddressLegal ; }
        set
        {
            SetComplexValue<Address, AddressWrapper>(_fieldAddressLegal, value);
            _fieldAddressLegal  = value;
        }
    }

	private AddressWrapper _fieldAddressPost;
	public AddressWrapper AddressPost 
    {
        get { return _fieldAddressPost ; }
        set
        {
            SetComplexValue<Address, AddressWrapper>(_fieldAddressPost, value);
            _fieldAddressPost  = value;
        }
    }

    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<BankDetailsWrapper> BankDetailsList { get; private set; }


    public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }


    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }


    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Form != null)
        {
            _fieldForm = new CompanyFormWrapper(Model.Form);
            RegisterComplex(Form);
        }

		if (Model.ParentCompany != null)
        {
            _fieldParentCompany = new CompanyWrapper(Model.ParentCompany);
            RegisterComplex(ParentCompany);
        }

		if (Model.AddressLegal != null)
        {
            _fieldAddressLegal = new AddressWrapper(Model.AddressLegal);
            RegisterComplex(AddressLegal);
        }

		if (Model.AddressPost != null)
        {
            _fieldAddressPost = new AddressWrapper(Model.AddressPost);
            RegisterComplex(AddressPost);
        }

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
