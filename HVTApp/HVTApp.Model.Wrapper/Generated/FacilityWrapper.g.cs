using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    protected FacilityWrapper(Facility model) : base(model) { }

	public static FacilityWrapper GetWrapper(Facility model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (FacilityWrapper)Repository.ModelWrapperDictionary[model];

		return new FacilityWrapper(model);
	}



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
        get { return FacilityTypeWrapper.GetWrapper(Model.Type); }
        set
        {
			var oldPropVal = Type;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


	public CompanyWrapper OwnerCompany 
    {
        get { return CompanyWrapper.GetWrapper(Model.OwnerCompany); }
        set
        {
			var oldPropVal = OwnerCompany;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }


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


    #endregion

    protected override void InitializeComplexProperties(Facility model)
    {

        Type = FacilityTypeWrapper.GetWrapper(model.Type);

        OwnerCompany = CompanyWrapper.GetWrapper(model.OwnerCompany);

        Address = AddressWrapper.GetWrapper(model.Address);

    }

  }
}
