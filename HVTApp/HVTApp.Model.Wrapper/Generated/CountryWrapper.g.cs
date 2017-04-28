using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class CountryWrapper : WrapperBase<Country>
  {
    public CountryWrapper() : base(new Country(), new Dictionary<IBaseEntity, object>()) { }
    public CountryWrapper(Country model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public CountryWrapper(Country model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public CountryWrapper(Country model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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

    #endregion

    #region CollectionProperties
    public IValidatableChangeTrackingCollection<DistrictWrapper> Districts { get; private set; }

    #endregion
    protected override void InitializeComplexProperties(Country model)
    {
        Capital = GetWrapper<LocalityWrapper, Locality>(model.Capital);
    }
  
    protected override void InitializeCollectionComplexProperties(Country model)
    {
      if (model.Districts == null) throw new ArgumentException("Districts cannot be null");
      Districts = new ValidatableChangeTrackingCollection<DistrictWrapper>(model.Districts.Select(e => GetWrapper<DistrictWrapper, District>(e)));
      RegisterCollection(Districts, model.Districts);

    }
  }
}
