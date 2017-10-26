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

	private FacilityTypeWrapper _fieldType;
	public FacilityTypeWrapper Type 
    {
        get { return _fieldType ; }
        set
        {
            SetComplexValue<FacilityType, FacilityTypeWrapper>(_fieldType, value);
            _fieldType  = value;
        }
    }

	private CompanyWrapper _fieldOwnerCompany;
	public CompanyWrapper OwnerCompany 
    {
        get { return _fieldOwnerCompany ; }
        set
        {
            SetComplexValue<Company, CompanyWrapper>(_fieldOwnerCompany, value);
            _fieldOwnerCompany  = value;
        }
    }

	private AddressWrapper _fieldAddress;
	public AddressWrapper Address 
    {
        get { return _fieldAddress ; }
        set
        {
            SetComplexValue<Address, AddressWrapper>(_fieldAddress, value);
            _fieldAddress  = value;
        }
    }

    #endregion

    public override void InitializeComplexProperties()
    {

		if (Model.Type != null)
        {
            _fieldType = new FacilityTypeWrapper(Model.Type);
            RegisterComplex(Type);
        }

		if (Model.OwnerCompany != null)
        {
            _fieldOwnerCompany = new CompanyWrapper(Model.OwnerCompany);
            RegisterComplex(OwnerCompany);
        }

		if (Model.Address != null)
        {
            _fieldAddress = new AddressWrapper(Model.Address);
            RegisterComplex(Address);
        }

    }

	}
}
	