using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictWrapper : WrapperBase<District>
  {
    public DistrictWrapper(District model) : base(model) { }
    public DistrictWrapper(District model, Dictionary<IBaseEntity, object> existsWrappers) : base(model, existsWrappers) { }

	public static DistrictWrapper GetWrapper(District model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (DistrictWrapper)Repository.ModelWrapperDictionary[model];

		return new DistrictWrapper(model);
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

	public CountryWrapper Country
	{
		get { return GetComplexProperty<Country, CountryWrapper>(nameof(Country)); }
		set { SetComplexProperty<Country, CountryWrapper>(value, nameof(Country)); }
	}


    #endregion

    protected override void InitializeComplexProperties(District model)
    {

        Country = CountryWrapper.GetWrapper(model.Country);

    }

  }
}
