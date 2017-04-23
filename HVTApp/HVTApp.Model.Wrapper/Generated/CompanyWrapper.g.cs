using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    public CompanyWrapper() : base(new Company()) { }
    public CompanyWrapper(Company model) : base(model) { }

//	public static CompanyWrapper GetWrapper()
//	{
//		return GetWrapper(new Company());
//	}
//
//	public static CompanyWrapper GetWrapper(Company model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (CompanyWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new CompanyWrapper(model);
//	}


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
	private CompanyFormWrapper _fieldForm;
	public CompanyFormWrapper Form 
    {
        get { return _fieldForm; }
        set
        {
			SetComplexProperty<CompanyFormWrapper, CompanyForm>(_fieldForm, value);
			_fieldForm = value;
        }
    }
    public CompanyFormWrapper FormOriginalValue { get; private set; }
    public bool FormIsChanged => GetIsChanged(nameof(Form));

	private CompanyWrapper _fieldParentCompany;
	public CompanyWrapper ParentCompany 
    {
        get { return _fieldParentCompany; }
        set
        {
			SetComplexProperty<CompanyWrapper, Company>(_fieldParentCompany, value);
			_fieldParentCompany = value;
        }
    }
    public CompanyWrapper ParentCompanyOriginalValue { get; private set; }
    public bool ParentCompanyIsChanged => GetIsChanged(nameof(ParentCompany));

	private AddressWrapper _fieldAddress;
	public AddressWrapper Address 
    {
        get { return _fieldAddress; }
        set
        {
			SetComplexProperty<AddressWrapper, Address>(_fieldAddress, value);
			_fieldAddress = value;
        }
    }
    public AddressWrapper AddressOriginalValue { get; private set; }
    public bool AddressIsChanged => GetIsChanged(nameof(Address));

	private BankDetailsWrapper _fieldBankDetails;
	public BankDetailsWrapper BankDetails 
    {
        get { return _fieldBankDetails; }
        set
        {
			SetComplexProperty<BankDetailsWrapper, BankDetails>(_fieldBankDetails, value);
			_fieldBankDetails = value;
        }
    }
    public BankDetailsWrapper BankDetailsOriginalValue { get; private set; }
    public bool BankDetailsIsChanged => GetIsChanged(nameof(BankDetails));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<CompanyWrapper> ChildCompanies { get; private set; }

    public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }

    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Company model)
    {
        Form = GetWrapper<CompanyFormWrapper, CompanyForm>(model.Form);
        ParentCompany = GetWrapper<CompanyWrapper, Company>(model.ParentCompany);
        Address = GetWrapper<AddressWrapper, Address>(model.Address);
        BankDetails = GetWrapper<BankDetailsWrapper, BankDetails>(model.BankDetails);
    }
  
    protected override void InitializeCollectionComplexProperties(Company model)
    {
      if (model.ChildCompanies == null) throw new ArgumentException("ChildCompanies cannot be null");
      ChildCompanies = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.ChildCompanies.Select(e => GetWrapper<CompanyWrapper, Company>(e)));
      RegisterCollection(ChildCompanies, model.ChildCompanies);

      if (model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
      ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(model.ActivityFilds.Select(e => GetWrapper<ActivityFieldWrapper, ActivityField>(e)));
      RegisterCollection(ActivityFilds, model.ActivityFilds);

      if (model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.Employees.Select(e => GetWrapper<EmployeeWrapper, Employee>(e)));
      RegisterCollection(Employees, model.Employees);

    }
  }
}
