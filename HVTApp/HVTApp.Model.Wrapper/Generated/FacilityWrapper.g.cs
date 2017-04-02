using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class FacilityWrapper : WrapperBase<Facility>
  {
    protected FacilityWrapper(Facility model) : base(model) { }
    //public FacilityWrapper(Facility model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

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

	private FacilityTypeWrapper _fieldType;
	public FacilityTypeWrapper Type 
    {
        get { return _fieldType; }
        set
        {
            if (Equals(_fieldType, value))
                return;

            UnRegisterComplexProperty(_fieldType);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldType = value;
        }
    }


	private CompanyWrapper _fieldOwnerCompany;
	public CompanyWrapper OwnerCompany 
    {
        get { return _fieldOwnerCompany; }
        set
        {
            if (Equals(_fieldOwnerCompany, value))
                return;

            UnRegisterComplexProperty(_fieldOwnerCompany);

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldOwnerCompany = value;
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

            RegisterComplexProperty(value);
            SetValue(value?.Model);
            _fieldAddress = value;
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
