using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    protected CompanyWrapper(Company model) : base(model) { }

	public static CompanyWrapper GetWrapper()
	{
		return GetWrapper(new Company());
	}

	public static CompanyWrapper GetWrapper(Company model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (CompanyWrapper)Repository.ModelWrapperDictionary[model];

		return new CompanyWrapper(model);
	}



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
        get { return CompanyFormWrapper.GetWrapper(Model.Form); }
        set
        {
			var oldPropVal = Form;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CompanyFormWrapper FormOriginalValue => CompanyFormWrapper.GetWrapper(GetOriginalValue<CompanyForm>(nameof(Form)));
    public bool FormIsChanged => GetIsChanged(nameof(Form));


	public CompanyWrapper ParentCompany 
    {
        get { return CompanyWrapper.GetWrapper(Model.ParentCompany); }
        set
        {
			var oldPropVal = ParentCompany;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CompanyWrapper ParentCompanyOriginalValue => CompanyWrapper.GetWrapper(GetOriginalValue<Company>(nameof(ParentCompany)));
    public bool ParentCompanyIsChanged => GetIsChanged(nameof(ParentCompany));


	public AddressWrapper Address 
    {
        get { return AddressWrapper.GetWrapper(Model.Address); }
        set
        {
			var oldPropVal = Address;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public AddressWrapper AddressOriginalValue => AddressWrapper.GetWrapper(GetOriginalValue<Address>(nameof(Address)));
    public bool AddressIsChanged => GetIsChanged(nameof(Address));


	public BankDetailsWrapper BankDetails 
    {
        get { return BankDetailsWrapper.GetWrapper(Model.BankDetails); }
        set
        {
			var oldPropVal = BankDetails;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public BankDetailsWrapper BankDetailsOriginalValue => BankDetailsWrapper.GetWrapper(GetOriginalValue<BankDetails>(nameof(BankDetails)));
    public bool BankDetailsIsChanged => GetIsChanged(nameof(BankDetails));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<CompanyWrapper> ChildCompanies { get; private set; }


    public IValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }


    public IValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Company model)
    {

        Form = CompanyFormWrapper.GetWrapper(model.Form);

        ParentCompany = CompanyWrapper.GetWrapper(model.ParentCompany);

        Address = AddressWrapper.GetWrapper(model.Address);

        BankDetails = BankDetailsWrapper.GetWrapper(model.BankDetails);

    }

  
    protected override void InitializeCollectionComplexProperties(Company model)
    {

      if (model.ChildCompanies == null) throw new ArgumentException("ChildCompanies cannot be null");
      ChildCompanies = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.ChildCompanies.Select(e => CompanyWrapper.GetWrapper(e)));
      RegisterCollection(ChildCompanies, model.ChildCompanies);


      if (model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
      ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(model.ActivityFilds.Select(e => ActivityFieldWrapper.GetWrapper(e)));
      RegisterCollection(ActivityFilds, model.ActivityFilds);


      if (model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.Employees.Select(e => EmployeeWrapper.GetWrapper(e)));
      RegisterCollection(Employees, model.Employees);


    }

  }
}
