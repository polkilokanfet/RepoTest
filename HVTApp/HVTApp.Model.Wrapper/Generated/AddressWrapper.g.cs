












using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class AddressWrapper : WrapperBase<Address>
  {
    protected AddressWrapper(Address model) : base(model) { }

	public static AddressWrapper GetWrapper()
	{
		return GetWrapper(new Address());
	}

	public static AddressWrapper GetWrapper(Address model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (AddressWrapper)Repository.ModelWrapperDictionary[model];

		return new AddressWrapper(model);
	}



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

	public LocalityWrapper Locality 
    {
        get { return LocalityWrapper.GetWrapper(Model.Locality); }
        set
        {
			var oldPropVal = Locality;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public LocalityWrapper LocalityOriginalValue => LocalityWrapper.GetWrapper(GetOriginalValue<Locality>(nameof(Locality)));
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));


    #endregion

    protected override void InitializeComplexProperties(Address model)
    {

        Locality = LocalityWrapper.GetWrapper(model.Locality);

    }

  }
}
