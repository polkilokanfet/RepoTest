using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class DistrictWrapper : WrapperBase<District>
  {
    public DistrictWrapper() : base(new District(), new Dictionary<IBaseEntity, object>()) { }
    public DistrictWrapper(District model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public DistrictWrapper(District model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public DistrictWrapper(District model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
        get { return GetComplexProperty<LocalityWrapper, Locality>(Model.Capital); }
        set { SetComplexProperty<LocalityWrapper, Locality>(Capital, value); }
    }

    public LocalityWrapper CapitalOriginalValue { get; private set; }
    public bool CapitalIsChanged => GetIsChanged(nameof(Capital));

	public CountryWrapper Country 
    {
        get { return GetComplexProperty<CountryWrapper, Country>(Model.Country); }
        set { SetComplexProperty<CountryWrapper, Country>(Country, value); }
    }

    public CountryWrapper CountryOriginalValue { get; private set; }
    public bool CountryIsChanged => GetIsChanged(nameof(Country));

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<RegionWrapper> Regions { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(District model)
    {
        Capital = GetWrapper<LocalityWrapper, Locality>(model.Capital);
        Country = GetWrapper<CountryWrapper, Country>(model.Country);
    }
  
    protected override void InitializeCollectionComplexProperties(District model)
    {
      if (model.Regions == null) throw new ArgumentException("Regions cannot be null");
      Regions = new ValidatableChangeTrackingCollection<RegionWrapper>(model.Regions.Select(e => GetWrapper<RegionWrapper, Region>(e)));
      RegisterCollection(Regions, model.Regions);

    }
  }
}
