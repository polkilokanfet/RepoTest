using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class AddressWrapper : WrapperBase<Address>
  {
    public AddressWrapper() : base(new Address()) { }
    public AddressWrapper(Address model) : base(model) { }

//	public static AddressWrapper GetWrapper()
//	{
//		return GetWrapper(new Address());
//	}
//
//	public static AddressWrapper GetWrapper(Address model)
//	{
//	    if (model == null)
//	        return null;
//
//		if (Repository.ExistsWrappers.ContainsKey(model))
//			return (AddressWrapper)Repository.ExistsWrappers[model];
//
//		return new AddressWrapper(model);
//	}


    #region SimpleProperties
    public System.String Description
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String DescriptionOriginalValue => GetOriginalValue<System.String>(nameof(Description));
    public bool DescriptionIsChanged => GetIsChanged(nameof(Description));

    public System.Int32 Id
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 IdOriginalValue => GetOriginalValue<System.Int32>(nameof(Id));
    public bool IdIsChanged => GetIsChanged(nameof(Id));

    #endregion

    #region ComplexProperties
	private LocalityWrapper _fieldLocality;
	public LocalityWrapper Locality 
    {
        get { return _fieldLocality; }
        set
        {
			SetComplexProperty<LocalityWrapper, Locality>(_fieldLocality, value);
			_fieldLocality = value;
        }
    }
    public LocalityWrapper LocalityOriginalValue { get; private set; }
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));

    #endregion
    protected override void InitializeComplexProperties(Address model)
    {
        Locality = GetWrapper<LocalityWrapper, Locality>(model.Locality);
    }
  }
}
