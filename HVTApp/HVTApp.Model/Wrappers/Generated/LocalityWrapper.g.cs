using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    private LocalityWrapper() : base(new Locality()) { }
    private LocalityWrapper(Locality model) : base(model) { }



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
        get { return GetComplexProperty<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(Model.StandartDeliveryPeriod); }
        set { SetComplexProperty<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(DeliveryPeriod, value); }
    }

    public StandartDeliveryPeriodWrapper DeliveryPeriodOriginalValue { get; private set; }
    public bool DeliveryPeriodIsChanged => GetIsChanged(nameof(DeliveryPeriod));


    #endregion

    public override void InitializeComplexProperties()
    {

        LocalityType = GetWrapper<LocalityTypeWrapper, LocalityType>(Model.LocalityType);

        Region = GetWrapper<RegionWrapper, Region>(Model.Region);

        DeliveryPeriod = GetWrapper<StandartDeliveryPeriodWrapper, StandartDeliveryPeriod>(Model.StandartDeliveryPeriod);

    }

  }
}
