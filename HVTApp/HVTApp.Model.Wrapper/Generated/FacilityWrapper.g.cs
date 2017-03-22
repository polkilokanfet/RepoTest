using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    public FacilityWrapper(Facility model) : base(model) { }
    public FacilityWrapper(Facility model, Dictionary<BaseEntity, object> existsWrappers) : base(model, existsWrappers) { }


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
		get { return GetComplexProperty<FacilityType, FacilityTypeWrapper>(nameof(Type)); }
		set { SetComplexProperty<FacilityType, FacilityTypeWrapper>(value, this.Type, nameof(Type)); }
	}


	public CompanyWrapper OwnerCompany
	{
		get { return GetComplexProperty<Company, CompanyWrapper>(nameof(OwnerCompany)); }
		set { SetComplexProperty<Company, CompanyWrapper>(value, this.OwnerCompany, nameof(OwnerCompany)); }
	}


	public AddressWrapper Address
	{
		get { return GetComplexProperty<Address, AddressWrapper>(nameof(Address)); }
		set { SetComplexProperty<Address, AddressWrapper>(value, this.Address, nameof(Address)); }
	}


    #endregion

    
    protected override void InitializeComplexProperties(Facility model)
    {

		if (model.Type != null)
		{
			if (ExistsWrappers.ContainsKey(model.Type))
			{
				Type = (FacilityTypeWrapper)ExistsWrappers[model.Type];
			}
			else
			{
				Type = new FacilityTypeWrapper(model.Type, ExistsWrappers);
				//ExistsWrappers.Add(model.Type, new FacilityTypeWrapper(model.Type, ExistsWrappers));
				RegisterComplexProperty(Type);
			}
		}


		if (model.OwnerCompany != null)
		{
			if (ExistsWrappers.ContainsKey(model.OwnerCompany))
			{
				OwnerCompany = (CompanyWrapper)ExistsWrappers[model.OwnerCompany];
			}
			else
			{
				OwnerCompany = new CompanyWrapper(model.OwnerCompany, ExistsWrappers);
				//ExistsWrappers.Add(model.OwnerCompany, new CompanyWrapper(model.OwnerCompany, ExistsWrappers));
				RegisterComplexProperty(OwnerCompany);
			}
		}


		if (model.Address != null)
		{
			if (ExistsWrappers.ContainsKey(model.Address))
			{
				Address = (AddressWrapper)ExistsWrappers[model.Address];
			}
			else
			{
				Address = new AddressWrapper(model.Address, ExistsWrappers);
				//ExistsWrappers.Add(model.Address, new AddressWrapper(model.Address, ExistsWrappers));
				RegisterComplexProperty(Address);
			}
		}


    }

  }
}
