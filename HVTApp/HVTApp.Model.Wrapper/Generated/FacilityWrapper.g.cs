using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    public FacilityWrapper() : base(new Facility()) { }
    public FacilityWrapper(Facility model) : base(model) { }
    public FacilityWrapper(Facility model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public FacilityWrapper(Facility model, IDictionary<IBaseEntity, object> dictionary) : base(model, new ExistsWrappers(dictionary)) { }



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

	public FacilityTypeWrapper Type 
    {
        get { return GetComplexProperty<FacilityTypeWrapper, FacilityType>(Model.Type); }
        set { SetComplexProperty<FacilityTypeWrapper, FacilityType>(Type, value); }
    }

    public FacilityTypeWrapper TypeOriginalValue { get; private set; }
    public bool TypeIsChanged => GetIsChanged(nameof(Type));


	public CompanyWrapper OwnerCompany 
    {
        get { return GetComplexProperty<CompanyWrapper, Company>(Model.OwnerCompany); }
        set { SetComplexProperty<CompanyWrapper, Company>(OwnerCompany, value); }
    }

    public CompanyWrapper OwnerCompanyOriginalValue { get; private set; }
    public bool OwnerCompanyIsChanged => GetIsChanged(nameof(OwnerCompany));


	public AddressWrapper Address 
    {
        get { return GetComplexProperty<AddressWrapper, Address>(Model.Address); }
        set { SetComplexProperty<AddressWrapper, Address>(Address, value); }
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
