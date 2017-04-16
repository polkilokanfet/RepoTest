using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class CountryWrapper : WrapperBase<Country>
  {
    protected CountryWrapper(Country model) : base(model) { }

	public static CountryWrapper GetWrapper()
	{
		return GetWrapper(new Country());
	}

	public static CountryWrapper GetWrapper(Country model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (CountryWrapper)Repository.ModelWrapperDictionary[model];

		return new CountryWrapper(model);
	}



    #region SimpleProperties

    public System.Int32 StandartDeliveryPeriod
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 StandartDeliveryPeriodOriginalValue => GetOriginalValue<System.Int32>(nameof(StandartDeliveryPeriod));
    public bool StandartDeliveryPeriodIsChanged => GetIsChanged(nameof(StandartDeliveryPeriod));


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

	public LocalityWrapper Capital 
    {
        get { return LocalityWrapper.GetWrapper(Model.Capital); }
        set
        {
			var oldPropVal = Capital;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public LocalityWrapper CapitalOriginalValue => LocalityWrapper.GetWrapper(GetOriginalValue<Locality>(nameof(Capital)));
    public bool CapitalIsChanged => GetIsChanged(nameof(Capital));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<DistrictWrapper> Districts { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(Country model)
    {

        Capital = LocalityWrapper.GetWrapper(model.Capital);

    }

  
    protected override void InitializeCollectionComplexProperties(Country model)
    {

      if (model.Districts == null) throw new ArgumentException("Districts cannot be null");
      Districts = new ValidatableChangeTrackingCollection<DistrictWrapper>(model.Districts.Select(e => DistrictWrapper.GetWrapper(e)));
      RegisterCollection(Districts, model.Districts);


    }

  }
}
