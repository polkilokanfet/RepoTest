using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    public CompanyWrapper(Company model) : base(model) { }
    public CompanyWrapper(Company model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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
		get { return GetComplexProperty<CompanyForm, CompanyFormWrapper>(nameof(Form)); }
		set { SetComplexProperty<CompanyForm, CompanyFormWrapper>(value, nameof(Form)); }
	}


	public CompanyWrapper ParentCompany
	{
		get { return GetComplexProperty<Company, CompanyWrapper>(nameof(ParentCompany)); }
		set { SetComplexProperty<Company, CompanyWrapper>(value, nameof(ParentCompany)); }
	}


	public AddressWrapper Address
	{
		get { return GetComplexProperty<Address, AddressWrapper>(nameof(Address)); }
		set { SetComplexProperty<Address, AddressWrapper>(value, nameof(Address)); }
	}


	public BankDetailsWrapper BankDetails
	{
		get { return GetComplexProperty<BankDetails, BankDetailsWrapper>(nameof(BankDetails)); }
		set { SetComplexProperty<BankDetails, BankDetailsWrapper>(value, nameof(BankDetails)); }
	}


    #endregion


    #region CollectionComplexProperties

    public ValidatableChangeTrackingCollection<CompanyWrapper> ChildCompanies { get; private set; }


    public ValidatableChangeTrackingCollection<ActivityFieldWrapper> ActivityFilds { get; private set; }


    public ValidatableChangeTrackingCollection<EmployeeWrapper> Employees { get; private set; }


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
      ChildCompanies = new ValidatableChangeTrackingCollection<CompanyWrapper>(model.ChildCompanies.Select(e => new CompanyWrapper(e, ExistsWrappers)));
      RegisterCollection(ChildCompanies, model.ChildCompanies);


      if (model.ActivityFilds == null) throw new ArgumentException("ActivityFilds cannot be null");
      ActivityFilds = new ValidatableChangeTrackingCollection<ActivityFieldWrapper>(model.ActivityFilds.Select(e => new ActivityFieldWrapper(e, ExistsWrappers)));
      RegisterCollection(ActivityFilds, model.ActivityFilds);


      if (model.Employees == null) throw new ArgumentException("Employees cannot be null");
      Employees = new ValidatableChangeTrackingCollection<EmployeeWrapper>(model.Employees.Select(e => new EmployeeWrapper(e, ExistsWrappers)));
      RegisterCollection(Employees, model.Employees);


    }

  }
}
