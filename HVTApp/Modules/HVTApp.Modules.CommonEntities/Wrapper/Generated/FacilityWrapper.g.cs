using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.UI.Wrapper
{
	public partial class FacilityWrapper : WrapperBase<Facility>
	{
	public FacilityWrapper(Facility model) : base(model) { }

	
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
        get { return GetWrapper<FacilityTypeWrapper>(); }
        set { SetComplexValue<FacilityType, FacilityTypeWrapper>(Type, value); }
    }

	public CompanyWrapper OwnerCompany 
    {
        get { return GetWrapper<CompanyWrapper>(); }
        set { SetComplexValue<Company, CompanyWrapper>(OwnerCompany, value); }
    }

	public AddressWrapper Address 
    {
        get { return GetWrapper<AddressWrapper>(); }
        set { SetComplexValue<Address, AddressWrapper>(Address, value); }
    }

    #endregion
    public override void InitializeComplexProperties()
    {
        InitializeComplexProperty<FacilityTypeWrapper>(nameof(Type), Model.Type == null ? null : new FacilityTypeWrapper(Model.Type));

        InitializeComplexProperty<CompanyWrapper>(nameof(OwnerCompany), Model.OwnerCompany == null ? null : new CompanyWrapper(Model.OwnerCompany));

        InitializeComplexProperty<AddressWrapper>(nameof(Address), Model.Address == null ? null : new AddressWrapper(Model.Address));

    }
	}
}
	