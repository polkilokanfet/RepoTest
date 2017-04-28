using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrapper
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    public LocalityWrapper() : base(new Locality(), new Dictionary<IBaseEntity, object>()) { }
    public LocalityWrapper(Locality model) : base(model, new Dictionary<IBaseEntity, object>()) { }
    //public LocalityWrapper(Locality model, ExistsWrappers existsWrappers) : base(model, existsWrappers) { }
    public LocalityWrapper(Locality model, IDictionary<IBaseEntity, object> dictionary) : base(model, dictionary) { }


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
	public LocalityTypeWrapper LocalityType 
    {
        get { return GetComplexProperty<LocalityTypeWrapper, LocalityType>(Model.LocalityType); }
        set { SetComplexProperty<LocalityTypeWrapper, LocalityType>(LocalityType, value); }
    }

    public LocalityTypeWrapper LocalityTypeOriginalValue { get; private set; }
    public bool LocalityTypeIsChanged => GetIsChanged(nameof(LocalityType));

	public RegionWrapper Region 
    {
        get { return GetComplexProperty<RegionWrapper, Region>(Model.Region); }
        set { SetComplexProperty<RegionWrapper, Region>(Region, value); }
    }

    public RegionWrapper RegionOriginalValue { get; private set; }
    public bool RegionIsChanged => GetIsChanged(nameof(Region));

	public StandartDeliveryPeriodWrapper DeliveryPeriod 
    {
        get { return GetComplexProperty<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(Model.DeliveryPeriod); }
        set { SetComplexProperty<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(DeliveryPeriod, value); }
    }

    public StandartDeliveryPeriodWrapper DeliveryPeriodOriginalValue { get; private set; }
    public bool DeliveryPeriodIsChanged => GetIsChanged(nameof(DeliveryPeriod));

    #endregion
    protected override void InitializeComplexProperties(Locality model)
    {
        LocalityType = GetWrapper<LocalityTypeWrapper, LocalityType>(model.LocalityType);
        Region = GetWrapper<RegionWrapper, Region>(model.Region);
        DeliveryPeriod = GetWrapper<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(model.DeliveryPeriod);
    }
  }
}
