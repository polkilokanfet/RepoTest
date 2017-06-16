using System;
using System.Collections.Generic;
using System.Linq;
using HVTApp.Model.POCOs;
using HVTApp.Infrastructure;

namespace HVTApp.Model.Wrappers
{
  public partial class StandartDeliveryPeriodWrapper : WrapperBase<StandartDeliveryPeriod>
  {
    public StandartDeliveryPeriodWrapper() : base(new StandartDeliveryPeriod()) { }
    public StandartDeliveryPeriodWrapper(StandartDeliveryPeriod model) : base(model) { }



    #region SimpleProperties

    public System.Int32 DeliveryPeriod
    {
      get { return GetValue<System.Int32>(); }
      set { SetValue(value); }
    }
    public System.Int32 DeliveryPeriodOriginalValue => GetOriginalValue<System.Int32>(nameof(DeliveryPeriod));
    public bool DeliveryPeriodIsChanged => GetIsChanged(nameof(DeliveryPeriod));


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
        get { return GetComplexProperty<LocalityWrapper, Locality>(Model.Locality); }
        set { SetComplexProperty<LocalityWrapper, Locality>(Locality, value); }
    }

    public LocalityWrapper LocalityOriginalValue { get; private set; }
    public bool LocalityIsChanged => GetIsChanged(nameof(Locality));


    #endregion

    public override void InitializeComplexProperties()
    {

        Locality = GetWrapper<LocalityWrapper, Locality>(Model.Locality);

    }

  }
}
