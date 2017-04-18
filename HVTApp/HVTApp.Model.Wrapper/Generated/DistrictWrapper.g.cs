using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictWrapper : WrapperBase<District>
  {
    protected DistrictWrapper(District model) : base(model) { }

	public static DistrictWrapper GetWrapper()
	{
		return GetWrapper(new District());
	}

	public static DistrictWrapper GetWrapper(District model)
	{
	    if (model == null)
	        return null;

		if (Repository.ModelWrapperDictionary.ContainsKey(model))
			return (DistrictWrapper)Repository.ModelWrapperDictionary[model];

		return new DistrictWrapper(model);
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


	public CountryWrapper Country 
    {
        get { return CountryWrapper.GetWrapper(Model.Country); }
        set
        {
			var oldPropVal = Country;
            UnRegisterComplexProperty(oldPropVal);
            RegisterComplexProperty(value);
            SetValue(value?.Model);
			OnComplexPropertyChanged(oldPropVal, value);
        }
    }
    public CountryWrapper CountryOriginalValue => CountryWrapper.GetWrapper(GetOriginalValue<Country>(nameof(Country)));
    public bool CountryIsChanged => GetIsChanged(nameof(Country));


    #endregion


    #region CollectionProperties

    public IValidatableChangeTrackingCollection<RegionWrapper> Regions { get; private set; }


    #endregion

    protected override void InitializeComplexProperties(District model)
    {

        Capital = LocalityWrapper.GetWrapper(model.Capital);

        Country = CountryWrapper.GetWrapper(model.Country);

    }

  
    protected override void InitializeCollectionComplexProperties(District model)
    {

      if (model.Regions == null) throw new ArgumentException("Regions cannot be null");
      Regions = new ValidatableChangeTrackingCollection<RegionWrapper>(model.Regions.Select(e => RegionWrapper.GetWrapper(e)));
      RegisterCollection(Regions, model.Regions);


    }

  }
}
