using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictWrapper : WrapperBase<District>
  {
    protected DistrictWrapper(District model) : base(model) { }

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

	private CountryWrapper _fieldCountry;
	public CountryWrapper Country 
    {
        get { return _fieldCountry; }
        set
        {
            if (Equals(_fieldCountry, value))
                return;

            UnRegisterComplexProperty(_fieldCountry);

            _fieldCountry = value;
            RegisterComplexProperty(value);
            SetValue(value?.Model);
        }
    }


    #endregion

    protected override void InitializeComplexProperties(District model)
    {

        Country = CountryWrapper.GetWrapper(model.Country);

    }

  }
}
