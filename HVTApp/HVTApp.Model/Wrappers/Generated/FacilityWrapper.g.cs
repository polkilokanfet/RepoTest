using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    private FacilityWrapper(IGetWrapper getWrapper) : base(new Facility(), getWrapper) { }
    private FacilityWrapper(Facility model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
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

    public override void InitializeComplexProperties()
    {

        Type = GetWrapper<FacilityTypeWrapper, FacilityType>(Model.Type);

        OwnerCompany = GetWrapper<CompanyWrapper, Company>(Model.OwnerCompany);

        Address = GetWrapper<AddressWrapper, Address>(Model.Address);

    }

  }
}
