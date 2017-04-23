using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    public FacilityWrapper() : base(new Facility()) { }
    public FacilityWrapper(Facility model) : base(model) { }

//	public static FacilityWrapper GetWrapper()
//	{
//		return GetWrapper(new Facility());
//	}
//
//	public static FacilityWrapper GetWrapper(Facility model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ModelWrapperDictionary.ContainsKey(model))
//			return (FacilityWrapper)Repository.ModelWrapperDictionary[model];
//
//		return new FacilityWrapper(model);
//	}


    #region SimpleProperties
    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private FacilityTypeWrapper _fieldType;
	public FacilityTypeWrapper Type 
    {
        get { return _fieldType; }
        set
        {
			SetComplexProperty<FacilityTypeWrapper, FacilityType>(_fieldType, value);
			_fieldType = value;
        }
    }
    public FacilityTypeWrapper TypeOriginalValue { get; private set; }
    public bool TypeIsChanged => GetIsChanged(nameof(Type));

	private CompanyWrapper _fieldOwnerCompany;
	public CompanyWrapper OwnerCompany 
    {
        get { return _fieldOwnerCompany; }
        set
        {
			SetComplexProperty<CompanyWrapper, Company>(_fieldOwnerCompany, value);
			_fieldOwnerCompany = value;
        }
    }
    public CompanyWrapper OwnerCompanyOriginalValue { get; private set; }
    public bool OwnerCompanyIsChanged => GetIsChanged(nameof(OwnerCompany));

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

    #endregion
    protected override void InitializeComplexProperties(Facility model)
    {
        Type = GetWrapper<FacilityTypeWrapper, FacilityType>(model.Type);
        OwnerCompany = GetWrapper<CompanyWrapper, Company>(model.OwnerCompany);
        Address = GetWrapper<AddressWrapper, Address>(model.Address);
    }
  }
}
