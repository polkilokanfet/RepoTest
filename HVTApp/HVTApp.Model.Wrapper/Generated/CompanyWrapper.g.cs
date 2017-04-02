using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CompanyWrapper : WrapperBase<Company>
  {
    protected CompanyWrapper(Company model) : base(model) { }
    //public CompanyWrapper(Company model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

	private CompanyFormWrapper _fieldForm;
	public CompanyFormWrapper Form 
    {
        get { return _fieldForm; }
        set
        {
            if (Equals(_fieldForm, value))
                return;

            UnRegisterComplexProperty(_fieldForm);

            _fieldForm = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private CompanyWrapper _fieldParentCompany;
	public CompanyWrapper ParentCompany 
    {
        get { return _fieldParentCompany; }
        set
        {
            if (Equals(_fieldParentCompany, value))
                return;

            UnRegisterComplexProperty(_fieldParentCompany);

            _fieldParentCompany = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private AddressWrapper _fieldAddress;
	public AddressWrapper Address 
    {
        get { return _fieldAddress; }
        set
        {
            if (Equals(_fieldAddress, value))
                return;

            UnRegisterComplexProperty(_fieldAddress);

            _fieldAddress = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


	private BankDetailsWrapper _fieldBankDetails;
	public BankDetailsWrapper BankDetails 
    {
        get { return _fieldBankDetails; }
        set
        {
            if (Equals(_fieldBankDetails, value))
                return;

            UnRegisterComplexProperty(_fieldBankDetails);

            _fieldBankDetails = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion


    #region CollectionProperties

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
