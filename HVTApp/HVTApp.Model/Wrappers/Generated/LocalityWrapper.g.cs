using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class LocalityWrapper : WrapperBase<Locality>
  {
    private LocalityWrapper(IGetWrapper getWrapper) : base(new Locality(), getWrapper) { }
    private LocalityWrapper(Locality model, IGetWrapper getWrapper) : base(model, getWrapper) { }



    #region SimpleProperties

    public System.String Name
    {
      get { return GetValue<System.String>(); }
      set { SetValue(value); }
    }
    public System.String NameOriginalValue => GetOriginalValue<System.String>(nameof(Name));
    public bool NameIsChanged => GetIsChanged(nameof(Name));


    public System.Nullable<System.Double> StandartDeliveryPeriod
    {
      get { return GetValue<System.Nullable<System.Double>>(); }
      set { SetValue(value); }
    }
    public System.Nullable<System.Double> StandartDeliveryPeriodOriginalValue => GetOriginalValue<System.Nullable<System.Double>>(nameof(StandartDeliveryPeriod));
    public bool StandartDeliveryPeriodIsChanged => GetIsChanged(nameof(StandartDeliveryPeriod));


    public System.Guid Id
    {
      get { return GetValue<System.Guid>(); }
      set { SetValue(value); }
    }
    public System.Guid IdOriginalValue => GetOriginalValue<System.Guid>(nameof(Id));
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


    #endregion

    public override void InitializeComplexProperties()
    {

        LocalityType = GetWrapper<LocalityTypeWrapper, LocalityType>(Model.LocalityType);

        Region = GetWrapper<RegionWrapper, Region>(Model.Region);

    }

  }
}
